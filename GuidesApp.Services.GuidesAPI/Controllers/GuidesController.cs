using GuidesApp.Services.GuidesAPI.Models;
using GuidesApp.Services.GuidesAPI.Models.Dto;
using GuidesApp.Services.GuidesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuidesApp.Services.GuidesAPI.Controllers
{
    [Route("api/[controller]")]
    public class GuidesController : Controller
    {
        private GuidesService _guideService;
        private ResponseDto _response;

        public GuidesController(GuidesService guideService)
        {
            _guideService = guideService;
            _response = new ResponseDto();
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAsync()
        {
            try
            {
                IEnumerable<Guide> guides = await _guideService.GetGuidesAsync();
                _response.Result = guides; 

            } catch (Exception ex)
            {
                _response.Result = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetAsync(int id)
        {
            try { 
                var guide = await _guideService.GetGuideAsync(id);
                _response.Result = guide;

                if (guide == null)
                {
                        _response.IsSuccess = false;
                        _response.Message = $"No Guide found with Id: '{id}'";
                        return NotFound(_response);
                }

            } catch (Exception ex)
            {
                _response.Result = false;
                _response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Post([FromBody] CreateGuideDto newGuide)
        {
            if (!ModelState.IsValid)
            {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
                var validationErrors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(kvp => kvp.Key,
                                  kvp => kvp.Value.Errors
                                  .Select(e => e.ErrorMessage).ToArray());
            #pragma warning restore CS8602 // Dereference of a possibly null reference.

                _response.Result = validationErrors;
                _response.IsSuccess = false;
                _response.Message = "Failed to validate";
                return BadRequest(_response);
            }
            try {

                Guide guide = await _guideService.PostGuideAsync(newGuide);
                _response.Result = guide;
                return CreatedAtAction(nameof(GetAsync), new { id = guide.GuideId }, _response);

            } catch (Exception ex) {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> PutAsync(int id, [FromBody]UpdateGuideDto updatedGuide)
        {
            var currentGuide = await _guideService.GetGuideAsync(id);

            if (currentGuide == null)
            {
                _response.Message = "No guide found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }


            _response.Result = await _guideService.PutGuideAsync(currentGuide, updatedGuide);
                
            return Ok(_response);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteAsync(int id)
        {
            var currentGuide = await _guideService.GetGuideAsync(id);

            if (currentGuide == null)
            {
                _response.Message = "No guide found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            await _guideService.DeleteGuideAsync(currentGuide);

            return Ok(_response);
        }
    }
}

