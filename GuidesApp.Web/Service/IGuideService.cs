using System;
using GuidesApp.Web.Models;

namespace GuidesApp.Web.Service
{
	public interface IGuideService
	{
		Task<ResponseDto?> GetGuideByIdAsync(int id);
		Task<ResponseDto?> GetAllGuidesAsync();
		Task<ResponseDto?> CreateGuideAsync(GuideDto guide);
		Task<ResponseDto?> UpdateGuideAsync(GuideDto guide);
		Task<ResponseDto?> DeleteGuideAsync(int id);
	}
}

