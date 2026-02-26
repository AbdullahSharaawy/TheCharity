using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.ItemImageDTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityBLL.Services.Repository;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatedItemImagesController : ControllerBase
    {
        private readonly IDonatedItemImageService _donatedItemImageService;
        public DonatedItemImagesController(IDonatedItemImageService donatedItemImageService)
        {
            _donatedItemImageService = donatedItemImageService;
        }

        [HttpGet("item/{id:int}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await _donatedItemImageService.GetItemImages(id);
            return HandleResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donatedItemImageService.GetItemImageById(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateItemImageDto dto)
        {
            var result = await _donatedItemImageService.AddItemImage(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donatedItemImageService.DeleteItemImage(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpGet("item/{id:int}/count")]
        public async Task<IActionResult> GetImageCount(int id)
        {
            var result = await _donatedItemImageService.GetItemImageCount(id);
            return HandleResponse(result);
        }

        [HttpGet("item/{id:int}/primary")]
        public async Task<IActionResult> GetPrimaryImage(int id)
        {
            var result = await _donatedItemImageService.GetPrimaryItemImage(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        //handel responce
        private IActionResult HandleResponse<T>(
            ServiceResponse<T> response,
            bool notFoundOnFailure = false)
        {
            if (!response.Success)
            {
                if (notFoundOnFailure)
                    return NotFound(response);

                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
