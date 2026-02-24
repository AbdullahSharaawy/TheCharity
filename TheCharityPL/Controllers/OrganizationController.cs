using Microsoft.AspNetCore.Mvc;
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Enums;
using TheCharityPL.Enums;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]//we must specifc roles and policies for each endpoint
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationQueryService _organizationQueryService;
        private readonly IOrganizationContactService _organizationContactService;
        public OrganizationController(IOrganizationService organizationService
            ,IOrganizationQueryService organizationQueryService
            ,IOrganizationContactService organizationContactService)
        {
            _organizationQueryService = organizationQueryService;
            _organizationService = organizationService;
            _organizationContactService = organizationContactService;
        }
        //basic crud operations
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _organizationService.GetAllOrganizations(includeDeleted);
            return HandleResponse(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _organizationService.GetOrganizationById(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _organizationService.GetOrganizationByIdWithDetails(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationDto dto)
        {
            var result = await _organizationService.AddOrganization(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrganizationDto dto)
        {
            dto.Id = id;
            var result = await _organizationService.UpdateOrganization(dto);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _organizationService.DeleteOrganization(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _organizationService.RestoreOrganization(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }
        /// /////
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            var result = await _organizationQueryService.SearchOrganizations(term);
            return HandleResponse(result);
        }
        [HttpGet("by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _organizationService.GetOrganizationByName(name);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            var result = await _organizationQueryService.GetOrganizationsDropDownList();
            return HandleResponse(result);
        }

        [HttpGet("statistics/total")]
        public async Task<IActionResult> GetTotalCount()
        {
            var result = await _organizationQueryService.GetTotalOrganizationsCount();
            return HandleResponse(result);
        }

        [HttpGet("statistics/active")]
        public async Task<IActionResult> GetActiveCount()
        {
            var result = await _organizationQueryService.GetActiveOrganizationsCount();
            return HandleResponse(result);
        }


        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent([FromQuery] int days=7)
        {
            var result = await _organizationQueryService.GetRecentlyRegisteredOrganizations(days);
            return HandleResponse(result);
        }

        [HttpGet("by-campaign-count")]
        public async Task<IActionResult> GetByCampaignCount([FromQuery] int min)
        {
            var result = await _organizationQueryService.GetByCampaignCount(min);
            return HandleResponse(result);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var result = await _organizationQueryService.GetDeletedOrganizations();
            return HandleResponse(result);
        }


        [HttpGet("filter/by-address")]
        public async Task<IActionResult> GetByAddress([FromQuery] string address)
        {
            var result = await _organizationService.GetAllOrganizationsByAddress(address);
            return HandleResponse(result);
        }

        [HttpGet("filter/by-contact-type")]
        public async Task<IActionResult> GetByContactType([FromQuery] ContactType type)
        {
            var result = await _organizationQueryService.GetByContactType(type);
            return HandleResponse(result);
        }

        [HttpGet("filter/by-status")]
        public async Task<IActionResult> GetByStatus([FromQuery]OrganizationStatusFilter filter)
        {
            ServiceResponse<IEnumerable<OrganizationResponseDto>> result;
            switch(filter)
            {
                case OrganizationStatusFilter.WithActiveCampaigns:
                    result = await _organizationQueryService.GetWithActiveCampaigns();
                    break;

                case OrganizationStatusFilter.WithCompletedCampaigns:
                    result = await _organizationQueryService.GetWithCompletedCampaigns();
                    break;

                case OrganizationStatusFilter.WithoutCampaigns:
                    result = await _organizationQueryService.GetWithoutCampaigns();
                    break;

                case OrganizationStatusFilter.WithoutPaymentInfo:
                    result = await _organizationQueryService.GetWithoutPaymentInfo();
                    break;

                default:
                    result = await _organizationService.GetAllOrganizations();
                    break;
            }
            return HandleResponse(result);
        }

        [HttpGet("filter/status-options")]
        public IActionResult GetStatusOptions()
        {
            var values = Enum.GetValues(typeof(OrganizationStatusFilter))
                             .Cast<OrganizationStatusFilter>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString()
                             });

            return Ok(values);
        }

        [HttpGet("{organizationId:int}/contacts")]
        public async Task<IActionResult> GetOrganizationContacts(int organizationId)
        {
            var result = await _organizationContactService.GetAllContactMethodsByOrganizationId(organizationId);
            return HandleResponse(result);
        }

        [HttpGet("{organizationId:int}/contacts/type/{type}")]
        public async Task<IActionResult> GetContactsByType(int organizationId, ContactType type)
        {
            var result = await _organizationContactService.GetContactMethodsByType(organizationId, type);
            return HandleResponse(result);
        }

        [HttpGet("contacts/{contactId:int}")]
        public async Task<IActionResult> GetContactById(int contactId)
        {
            var result = await _organizationContactService.GetContactMethodById(contactId);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost("contacts")]
        public async Task<IActionResult> AddContact([FromBody] CreateOrganizationContactMethodDto dto)
        {
            var result = await _organizationContactService.AddContactMethod(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetContactById), new { contactId = result.Data }, result);
        }

        [HttpPut("contacts/{contactId:int}")]
        public async Task<IActionResult> UpdateContact(int contactId, [FromBody] UpdateOrganizationContactMethodDto dto)
        {
            dto.Id = contactId;
            var result = await _organizationContactService.UpdateContactMethod(dto);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpDelete("contacts/{contactId:int}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            var result = await _organizationContactService.DeleteContactMethod(contactId);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("contacts/{contactId:int}/restore")]
        public async Task<IActionResult> RestoreContact(int contactId)
        {
            var result = await _organizationContactService.RestoreContactMethod(contactId);
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
