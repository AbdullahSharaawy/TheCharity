using Microsoft.AspNetCore.Mvc;
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Enums;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationContactsController : ControllerBase
    {
        private readonly IOrganizationContactService _organizationContactService;

        public OrganizationContactsController(IOrganizationContactService organizationContactService)
        {
            _organizationContactService = organizationContactService;
        }
        [HttpGet("organization/{id:int}")]
        public async Task<IActionResult> GetAllByOrganizationId(int id)
        {
            var result = await _organizationContactService.GetAllContactMethodsByOrganizationId(id);
            return HandleResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _organizationContactService.GetContactMethodById(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpGet("organization/{id:int}/filter")]
        public async Task<IActionResult> GetContactsByType(int id, [FromQuery] ContactType type)
        {
            var result = await _organizationContactService.GetContactMethodsByType(id, type);
            return HandleResponse(result);
        }

        [HttpGet("organization/{id:int}/count")]
        public async Task<IActionResult> GetCountByType(int id, [FromQuery] ContactType type)
        {
            var result = await _organizationContactService.GetContactMethodCountByType(id, type);
            return HandleResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] CreateOrganizationContactMethodDto dto)
        {
            var result = await _organizationContactService.AddContactMethod(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateOrganizationContactMethodDto dto)
        {
            dto.Id = id;
            var result = await _organizationContactService.UpdateContactMethod(dto);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _organizationContactService.DeleteContactMethod(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/restore")] 
        public async Task<IActionResult> RestoreContact(int id)
        {
            var result = await _organizationContactService.RestoreContactMethod(id);
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
