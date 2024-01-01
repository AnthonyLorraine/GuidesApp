using System;
using GuidesApp.Web.Models;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;
using Newtonsoft.Json;

namespace GuidesApp.Web.Service
{
	public class GuideService : IGuideService
	{
        private IBaseService _baseService;

		public GuideService(IBaseService baseService)
		{
            _baseService = baseService;
		}

        public async Task<ResponseDto?> CreateGuideAsync(CreateGuideDto guide)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.GuideAPIBase + "/api/guides",
                Data = guide
            });
        }

        public async Task<ResponseDto?> DeleteGuideAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.GuideAPIBase + $"/api/guides/{id}",
            });
        }

        public async Task<ResponseDto?> GetAllGuidesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.GuideAPIBase + "/api/guides"
            }) ;
        }

        public async Task<ResponseDto?> GetGuideByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.GuideAPIBase + $"/api/guides/{id}",
            });
        }

        public async Task<ResponseDto?> UpdateGuideAsync(UpdateGuideDto guide)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.GuideAPIBase + $"/api/guides/{guide.GuideId}",
                Data = guide
            }); ;
        }
    }
}

