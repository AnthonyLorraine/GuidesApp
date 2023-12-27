using System.Net;
using GuidesApp.Web.Models;
using GuidesApp.Web.Service;
using Moq;
using Newtonsoft.Json;
using Xunit;
using static GuidesApp.Web.Utility.StaticDetails;

namespace GuidesApp.Web.Tests.Services
{
	public class BaseServiceTests
	{
        [Fact]
        public async Task SendAsync_PostRequest_SuccessfulResponse()
        {
            // Arrange
            
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClientMock = new Mock<HttpClient>();

            var baseService = new BaseService(httpClientFactoryMock.Object);

            var requestDto = new RequestDto
            {
                ApiType = ApiType.POST,
                Url = "https://example.com/api/resource",
                Data = new { Title = "Title", Subtitle = "Subtitle", Content = "Content"} // Replace with your actual data
            };

            var expectedResponse = new ResponseDto { IsSuccess = true, Message = "Success" };

            httpClientFactoryMock.Setup(factory => factory.CreateClient("GuideAPI"))
                .Returns(httpClientMock.Object);

            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            var result = await baseService.SendAsync(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Success", result.Message);
        }

    }
}

