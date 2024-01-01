using System;
using GuidesApp.Web.Models;
using GuidesApp.Web.Service;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;
using Moq;
using Xunit;

namespace GuidesApp.Web.Tests.Services
{
	public class GuideServiceTests
	{
        [Fact]
        public async Task CreateGuideAsync_ValidGuide_SuccessfulResponse()
        {
            var baseServiceMock = new Mock<IBaseService>();
            var guideService = new GuideService(baseServiceMock.Object);

            var guide = new CreateGuideDto { Content = "New Guide", Title = "New Guide Title", Subtitle = "Subtitle New"};

            baseServiceMock.Setup(service => service.SendAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(new ResponseDto { IsSuccess = true, Message = "Guide created successfully" });

           
            var result = await guideService.CreateGuideAsync(guide);

            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Guide created successfully", result.Message);

            baseServiceMock.Verify(
                service => service.SendAsync(It.Is<RequestDto>(request =>
                    request.ApiType == StaticDetails.ApiType.POST &&
                    request.Url == StaticDetails.GuideAPIBase + "/api/guides" &&
                    request.Data == guide)),
                Times.Once); // Ensure it was called exactly once
        }
        [Fact]
        public async Task UpdateGuideAsync_ValidGuide_SuccessfulResponse()
        {
            // Arrange
            var baseServiceMock = new Mock<IBaseService>();
            var guideService = new GuideService(baseServiceMock.Object);

            var guide = new GuideDto { /* initialize guide properties */ };

            // Expectation: The SendAsync method should be called with the correct RequestDto, including the PUT method.
            baseServiceMock.Setup(service => service.SendAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(new ResponseDto { IsSuccess = true, Message = "Guide updated successfully" });

            // Act
            var result = await guideService.UpdateGuideAsync(guide);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Guide updated successfully", result.Message);

            // Verify that the SendAsync method was called with the expected parameters.
            baseServiceMock.Verify(
                service => service.SendAsync(It.Is<RequestDto>(request =>
                    request.ApiType == StaticDetails.ApiType.PUT &&
                    request.Url == StaticDetails.GuideAPIBase + "/api/guides/" + guide.GuideId &&
                    request.Data == guide)),
                Times.Once); // Ensure it was called exactly once
        }

        [Fact]
        public async Task DeleteGuideAsync_ValidId_SuccessfulResponse()
        {
            // Arrange
            var baseServiceMock = new Mock<IBaseService>();
            var guideService = new GuideService(baseServiceMock.Object);

            var guideId = 1;

            // Expectation: The SendAsync method should be called with the correct RequestDto, including the DELETE method.
            baseServiceMock.Setup(service => service.SendAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(new ResponseDto { IsSuccess = true, Message = "Guide deleted successfully" });

            // Act
            var result = await guideService.DeleteGuideAsync(guideId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Guide deleted successfully", result.Message);

            // Verify that the SendAsync method was called with the expected parameters.
            baseServiceMock.Verify(
                service => service.SendAsync(It.Is<RequestDto>(request =>
                    request.ApiType == StaticDetails.ApiType.DELETE &&
                    request.Url == $"{StaticDetails.GuideAPIBase}/api/guides/{guideId}")),
                Times.Once); // Ensure it was called exactly once
        }

        [Fact]
        public async Task GetAllGuidesAsync_ValidRequest_SuccessfulResponse()
        {
            // Arrange
            var baseServiceMock = new Mock<IBaseService>();
            var guideService = new GuideService(baseServiceMock.Object);

            // Expectation: The SendAsync method should be called with the correct RequestDto, including the GET method.
            baseServiceMock.Setup(service => service.SendAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(new ResponseDto { IsSuccess = true, Message = "Guides retrieved successfully" });

            // Act
            var result = await guideService.GetAllGuidesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Guides retrieved successfully", result.Message);

            // Verify that the SendAsync method was called with the expected parameters.
            baseServiceMock.Verify(
                service => service.SendAsync(It.Is<RequestDto>(request =>
                    request.ApiType == StaticDetails.ApiType.GET &&
                    request.Url == $"{StaticDetails.GuideAPIBase}/api/guides")),
                Times.Once); // Ensure it was called exactly once
        }

        [Fact]
        public async Task GetGuideByIdAsync_ValidId_SuccessfulResponse()
        {
            // Arrange
            var baseServiceMock = new Mock<IBaseService>();
            var guideService = new GuideService(baseServiceMock.Object);

            var guideId = 1;

            // Expectation: The SendAsync method should be called with the correct RequestDto, including the GET method.
            baseServiceMock.Setup(service => service.SendAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(new ResponseDto { IsSuccess = true, Message = "Guide retrieved successfully" });

            // Act
            var result = await guideService.GetGuideByIdAsync(guideId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Guide retrieved successfully", result.Message);

            // Verify that the SendAsync method was called with the expected parameters.
            baseServiceMock.Verify(
                service => service.SendAsync(It.Is<RequestDto>(request =>
                    request.ApiType == StaticDetails.ApiType.GET &&
                    request.Url == $"{StaticDetails.GuideAPIBase}/api/guides/{guideId}")),
                Times.Once); // Ensure it was called exactly once
        }
    }
}

