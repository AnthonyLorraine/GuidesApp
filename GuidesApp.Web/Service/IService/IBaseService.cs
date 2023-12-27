using System;
using GuidesApp.Web.Models;

namespace GuidesApp.Web.Service.IService
{
	public interface IBaseService
	{
		Task<ResponseDto?> SendAsync(RequestDto requestDto);
	}
}

