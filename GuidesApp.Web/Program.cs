using GuidesApp.Web.Service;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

