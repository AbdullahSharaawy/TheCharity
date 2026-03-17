using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.AttachmentDTOs;
using TheCharityBLL.Services.Abstraction.DonatedItems;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatedItemAttachmentsController : ControllerBase
    {
        private readonly IDonatedItemAttachmentService _donatedItemAttachmentService;
        public DonatedItemAttachmentsController(IDonatedItemAttachmentService donatedItemAttachmentService)
        {
            _donatedItemAttachmentService = donatedItemAttachmentService;
        }

        [HttpGet("item/{id:int}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await _donatedItemAttachmentService.GetItemAttachments(id);
            return HandleResponse(result);
        }

        [HttpGet("recipient/{id:int}")]
        public async Task<IActionResult> GetAllRecipientAttachments(int id)
        {
            var result = await _donatedItemAttachmentService.GetRecipientAttachments(id);
            return HandleResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donatedItemAttachmentService.GetAttachmentById(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAttachmentDto dto)
        {
            var result = await _donatedItemAttachmentService.AddAttachment(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donatedItemAttachmentService.DeleteAttachment(id);
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
