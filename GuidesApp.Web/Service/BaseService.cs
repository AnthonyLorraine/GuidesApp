
using GuidesApp.Web.Models;
using GuidesApp.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;
using static GuidesApp.Web.Utility.StaticDetails;
using System.Net;

namespace GuidesApp.Web.Service
{
	public class BaseService : IBaseService
	{
        private readonly IHttpClientFactory _httpClientFactory;
		public BaseService(IHttpClientFactory httpClientFactory)
		{
            _httpClientFactory = httpClientFactory;

        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try { 
                HttpClient client = _httpClientFactory.CreateClient("GuideAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                // token

                if (requestDto.Url == null)
                {
                    throw new Exception("Request URL is null");
                }

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(
                        requestDto.Data),
                        Encoding.UTF8,
                        "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);
                var responseContent = await apiResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ResponseDto>(responseContent);

            } catch (Exception ex)
            {
                return new() { IsSuccess = false, Message = ex.Message.ToString() };
            }

        }
    }
}

