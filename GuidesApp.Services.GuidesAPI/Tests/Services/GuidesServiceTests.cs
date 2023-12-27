using GuidesApp.Services.GuidesAPI.Data;
using GuidesApp.Services.GuidesAPI.Models;
using GuidesApp.Services.GuidesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace GuidesApp.Services.GuidesAPI.Tests.Services
{
    public class GuidesServiceTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        public GuidesServiceTests()
		{
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(_dbContextOptions);
        }

        [Fact]
        public async Task GetGuidesAsync_ShouldReturnGuides()
        {
            using var context = new AppDbContext(_dbContextOptions);
            context.Guides.Add(new Guide { Title = "Test Guide 1", Subtitle = "Subtitle 1", Content = "Content 1" });
            context.Guides.Add(new Guide { Title = "Test Guide 2", Subtitle = "Subtitle 2", Content = "Content 2" });
            context.SaveChanges();

            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            var guides = await service.GetGuidesAsync();

            Assert.NotNull(guides);
            Assert.Equal(2, guides.Count());
        }
        [Fact]
        public async Task GetGuidesAsync_ShouldReturnNoGuides()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            var guides = await service.GetGuidesAsync();
       
            Assert.NotNull(guides);
            Assert.Empty(guides);
        }
        [Fact]
        public async Task GetGuidesByIdAsync_ShouldReturnIdById()
        {
            using var context = new AppDbContext(_dbContextOptions);
            context.Guides.Add(new Guide { Title = "Test Guide 1", Subtitle = "Subtitle 1", Content = "Content 1" });
            context.SaveChanges();
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            Guide guide = await service.GetGuideAsync(1);

            Assert.NotNull(guide);
            Assert.Equal(1, guide.GuideId);
        }
        [Fact]
        public async Task GetGuidesByIdAsync_ShouldReturnNullForNonExistentId()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            Guide guide = await service.GetGuideAsync(999);

            Assert.Null(guide);
        }
        [Fact]
        public async Task PostGuideAsync_ShouldCreateGuide()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            Guide guide = new()
            {
                Title = "Test Title",
                Subtitle = "Test Subtitle",
                Content = "Test Content"
            };

            Guide createdGuide = await service.PostGuideAsync(guide);
            Assert.Equal(1, createdGuide.GuideId);
            Assert.Equal(createdGuide.Title, guide.Title);
            Assert.Equal(createdGuide.Subtitle, guide.Subtitle);
            Assert.Equal(createdGuide.Content, guide.Content);
        }
        [Fact]
        public async Task PostGuideAsync_ShouldNotCreateGuideMissingTitle()
        {
            using var context = new AppDbContext(_dbContextOptions);
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());

            Guide guide = new()
            {
                Subtitle = "Test Subtitle",
                Content = "Test Content"
            };
            var exception = await Assert.ThrowsAsync<DbUpdateException>(() => service.PostGuideAsync(guide));
            Assert.Contains("Required properties '{'Title'}'", exception.Message);
        }
        [Fact]
        public async Task PutGuideAsync_ShouldUpdateGuide()
        {
            using var context = new AppDbContext(_dbContextOptions);
            context.Guides.Add(new Guide { Title = "Test Guide 1", Subtitle = "Subtitle 1", Content = "Content 1" });
            context.SaveChanges();
            var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());
            Guide currentGuide = await service.GetGuideAsync(1);
            Guide updatedGuide = new()
            {
                Title = "Test Guide 2",
                Subtitle = "Subtitle 2",
                Content = "Content 2"
            };

            Assert.NotEqual(currentGuide.Title, updatedGuide.Title);
            Assert.NotEqual(currentGuide.Content, updatedGuide.Content);
            Assert.NotEqual(currentGuide.Subtitle, updatedGuide.Subtitle);

            var changedGuide = await service.PutGuideAsync(currentGuide, updatedGuide);
            Assert.NotNull(changedGuide);
            Assert.Equal(changedGuide.Title, updatedGuide.Title);
            Assert.Equal(changedGuide.Content, updatedGuide.Content);
            Assert.Equal(changedGuide.Subtitle, updatedGuide.Subtitle);
        }
        //[Fact]
        //public async Task PutGuideAsync_ShouldReturnNullForNonExistentGuide()
        //{
        //    using var context = new AppDbContext(_dbContextOptions);
        //    var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());
        //    context.Guides.Add(new Guide { Title = "Test Guide 1", Subtitle = "Subtitle 1", Content = "Content 1" });
        //    Guide currentGuide = await service.GetGuideAsync(1);
            
        //    Guide updatedGuide = new()
        //    {
        //        GuideId = 999,
        //        Title = "Test Guide 2",
        //        Subtitle = "Subtitle 2",
        //        Content = "Content 2"
        //    };

        //    var changedGuide = await service.PutGuideAsync(currentGuide, updatedGuide);
            
        //    Assert.Null(changedGuide);
        //}
        //[Fact]
        //public async Task DeleteGuideAsync_ShouldDeleteGuide()
        //{
        //    using var context = new AppDbContext(_dbContextOptions);
        //    context.Guides.Add(new Guide { Title = "Test Guide 1", Subtitle = "Subtitle 1", Content = "Content 1" });
        //    context.SaveChanges();
        //    var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());
        //    Guide guide = await service.GetGuideAsync(1);
        //    Assert.NotNull(guide);
        //    Assert.Equal(guide.GuideId, 1);
   
        //    var returnCode = await service.DeleteGuideAsync(guide);
        //    Guide deletedGuide = await service.GetGuideAsync(1);
        //    Assert.NotNull(deletedGuide);
        //}
        //[Fact]
        //public async Task DeleteGuideAsync_ShouldReturnNullForNonExistentGuide()
        //{
        //    using var context = new AppDbContext(_dbContextOptions);
        //    var service = new GuidesService(context, Mock.Of<ILogger<GuidesService>>());
        //    Guide guide = new()
        //    {
        //        GuideId = 999,
        //        Title = "Test Guide 2",
        //        Subtitle = "Subtitle 2",
        //        Content = "Content 2"
        //    };
        //    var returnCode = await service.DeleteGuideAsync(guide);
        //    Console.WriteLine(returnCode);
        //}

        public void Dispose()
		{

		}
	}
}

