using System;
using GuidesApp.Web.Models;

namespace GuidesApp.Web.Service.IService
{
	public interface IGuideService
	{
		Task<ResponseDto?> GetGuideByIdAsync(int id);
		Task<ResponseDto?> GetAllGuidesAsync();
		Task<ResponseDto?> CreateGuideAsync(CreateGuideDto guide);
		Task<ResponseDto?> UpdateGuideAsync(UpdateGuideDto guide);
		Task<ResponseDto?> DeleteGuideAsync(int id);
	}
}

