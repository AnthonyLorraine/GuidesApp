using GuidesApp.Web.Service;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Login";
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddHttpClient<IGuideService, GuideService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();

StaticDetails.GuideAPIBase = builder.Configuration["ServiceUrls:GuideUrl"];
StaticDetails.AuthAPIBase = builder.Configuration["ServiceUrls:AuthUrl"];

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IGuideService, GuideService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

