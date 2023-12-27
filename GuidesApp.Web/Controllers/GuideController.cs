﻿using GuidesApp.Web.Models;
using GuidesApp.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuidesApp.Web.Controllers
{
   
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;
        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        public async Task<IActionResult> Index()
        {
            List<GuideDto>? guides = new();
            try
            {

                ResponseDto? response = await _guideService.GetAllGuidesAsync();

                if (response != null && response.IsSuccess)
                {
                    string? resultJson = Convert.ToString(response.Result);
                    if (!string.IsNullOrEmpty(resultJson))
                    {
                        guides = JsonConvert.DeserializeObject<List<GuideDto>>(resultJson);
                    }
                    else
                    {
                        throw new Exception("Response result contains no data.");
                    }
                }
                
                return View(guides);
            }
            catch (Exception)
            {
                return View(guides);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            GuideDto? guide = new();

            try
            {

                ResponseDto? response = await _guideService.GetGuideByIdAsync(id);

                if (response != null && response.IsSuccess)
                {
                    string? resultJson = Convert.ToString(response.Result);
                    if (!string.IsNullOrEmpty(resultJson))
                    {
                        guide = JsonConvert.DeserializeObject<GuideDto>(resultJson);
                    }
                    else
                    {
                        throw new Exception("Response result contains no data.");
                    }
                }

                return View(guide);
            }
            catch (Exception)
            {
                return View(guide);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GuideDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuideDto guide)
        {
            GuideDto? newGuide = new();
            try
            {

                ResponseDto? response = await _guideService.CreateGuideAsync(guide);

                if (response != null && response.IsSuccess)
                {
                    string? resultJson = Convert.ToString(response.Result);
                    if (!string.IsNullOrEmpty(resultJson))
                    {
                        newGuide = JsonConvert.DeserializeObject<GuideDto>(resultJson);
                        if (newGuide == null)
                        {
                            throw new Exception("Couldnt convert JSON to GuideDTO" + resultJson);
                        }
                    }
                    else
                    {
                        throw new Exception("Response result contains no data.");
                    }
                }

                return RedirectToAction("Detail", new {id = newGuide.GuideId });
            }
            catch (Exception)
            {
                return View(guide);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDto? response = await _guideService.DeleteGuideAsync(id);

            if (response != null && !response.IsSuccess)
            {
                TempData["ErrorMessage"] = $"Failed to delete guide {id}. {response.Message}";
            } else
            {
                TempData["SuccessMessage"] = "Guide has been deleted";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GuideDto? guide = new();

            ResponseDto? response = await _guideService.GetGuideByIdAsync(id);
            if (response != null && response.IsSuccess)
            {
                string? resultJson = Convert.ToString(response.Result);
                if (!string.IsNullOrEmpty(resultJson))
                {
                    guide = JsonConvert.DeserializeObject<GuideDto>(resultJson);
                }
                else
                {
                    throw new Exception("Response result contains no data.");
                }
            }

            return View(guide);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GuideDto guide)
        {
            GuideDto updatedGuide = new();
            guide.GuideId = id;

            try
            {

                ResponseDto? response = await _guideService.UpdateGuideAsync(guide);

                if (response != null && response.IsSuccess)
                {
                    string? resultJson = Convert.ToString(response.Result);
                    if (!string.IsNullOrEmpty(resultJson))
                    {
                        updatedGuide = JsonConvert.DeserializeObject<GuideDto>(resultJson);
                        if (updatedGuide == null)
                        {
                            TempData["ErrorMessage"] = $"Failed to edit guide {id}. {response.Message}";
                            throw new Exception();
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Failed to edit guide {id}. Response contained no data";
                        throw new Exception();
                    }
                } else
                {
                    TempData["ErrorMessage"] = $"Failed to edit guide {id}. Response was null";
                    throw new Exception();
                }

                return RedirectToAction("Detail", new { id = updatedGuide.GuideId });
            }
            catch (Exception)
            {
                return View(guide);
            }
        }

    }
}
