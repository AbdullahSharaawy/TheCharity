using Microsoft.AspNetCore.Mvc;
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Enums;
using TheCharityPL.Enums;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatedItemsController : ControllerBase
    {
        private readonly IDonatedItemService _donatedItemService;
        private readonly IDonatedItemQueryService _donatedItemQueryService;

        public DonatedItemsController(IDonatedItemService donatedItemService,
            IDonatedItemQueryService donatedItemQueryService)
        {
            _donatedItemService = donatedItemService;
            _donatedItemQueryService = donatedItemQueryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var response = await _donatedItemService.GetAllDonatedItems(includeDeleted);
            return HandleResponse(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _donatedItemService.GetDonatedItemById(id);
            return HandleResponse(response, notFoundOnFailure: true);
        }

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await _donatedItemService.GetDonatedItemByIdWithDetails(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonatedItemDto dto)
        {
            var result = await _donatedItemService.AddDonatedItem(dto);
            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonatedItemDto dto)
        {
            dto.Id = id;
            var result = await _donatedItemService.UpdateDonatedItem(dto);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donatedItemService.DeleteDonatedItem(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _donatedItemService.RestoreDonatedItem(id);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/availability")]
        public async Task<IActionResult> UpdateAvailability(int id, [FromQuery] bool isAvailable)
        {
            var result = await _donatedItemService.UpdateItemAvailability(id, isAvailable);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/category")]
        public async Task<IActionResult> UpdateCategory(int id, [FromQuery] ItemCategory category)
        {
            var result = await _donatedItemService.UpdateItemCategory(id, category);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/transfer/{newOrgId:int}")]
        public async Task<IActionResult> TransferToOrganization(int id, int newOrgId)
        {
            var result = await _donatedItemService.TransferItemToOrganization(id, newOrgId);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPatch("{id:int}/donor/{newDonorId}")]
        public async Task<IActionResult> UpdateDonor(int id, [FromQuery] string newDonorId)
        {
            var result = await _donatedItemService.UpdateItemDonor(id, newDonorId);
            return HandleResponse(result, notFoundOnFailure: true);
        }

        [HttpPost("bulk/update-category")]
        public async Task<IActionResult> BulkUpdateCategory([FromQuery] ItemCategory oldCat, [FromQuery] ItemCategory newCat)
        {
            var result = await _donatedItemService.BulkUpdateItemCategories(oldCat, newCat);
            return HandleResponse(result);
        }

        [HttpPatch("bulk/mark-unavailable/{organizationId:int}")]
        public async Task<IActionResult> BulkMarkAsUnavailable(int organizationId)
        {
            var result = await _donatedItemService.BulkMarkItemsAsUnavailable(organizationId);
            return HandleResponse(result);
        }

        [HttpDelete("bulk/clean-old-items")]
        public async Task<IActionResult> DeleteOldItems([FromQuery] int days = 365)
        {
            var result = await _donatedItemService.DeleteOldDonatedItems(days);
            return HandleResponse(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var response = await _donatedItemQueryService.SearchDonatedItems(searchTerm);
            return HandleResponse(response);
        }

        [HttpGet("search-advanced")]
        public async Task<IActionResult> SearchAdvanced([FromQuery] string searchTerm, [FromQuery] ItemCategory? category)
        {
            var response = await _donatedItemQueryService.SearchAvailableItemsByCategory(searchTerm, category);
            return HandleResponse(response);
        }

        [HttpGet("filter/organization/{organizationId:int}")]
        public async Task<IActionResult> GetByOrganization(int organizationId, [FromQuery] ItemCategory? category)
        {
            if (category.HasValue)
            {
                var filteredResponse = await _donatedItemQueryService.GetItemsByOrganizationAndCategory(organizationId, category.Value);
                return HandleResponse(filteredResponse);
            }

            var response = await _donatedItemQueryService.GetDonatedItemsByOrganization(organizationId);
            return HandleResponse(response);
        }

        [HttpGet("filter/donor/{donorId}")]
        public async Task<IActionResult> GetByDonor(string donorId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                var rangeResponse = await _donatedItemQueryService.GetDonatedItemsByDonorAndDateRange(donorId, startDate.Value, endDate.Value);
                return HandleResponse(rangeResponse);
            }

            var response = await _donatedItemQueryService.GetDonatedItemsByDonor(donorId);
            return HandleResponse(response);
        }

        [HttpGet("filter/category/{category}")]
        public async Task<IActionResult> GetByCategory(ItemCategory category)
        {
            var response = await _donatedItemQueryService.GetDonatedItemsByCategory(category);
            return HandleResponse(response);
        }

        [HttpGet("inventory/{id:int}")]
        public async Task<IActionResult> GetInventory(int id)
        {
            var response = await _donatedItemQueryService.GetOrganizationInventory(id);
            return HandleResponse(response);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent([FromQuery] int days = 7)
        {
            var response = await _donatedItemQueryService.GetRecentDonatedItems(days);
            return HandleResponse(response);
        }

        [HttpGet("filter/date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var response = await _donatedItemQueryService.GetDonatedItemsByDateRange(start, end);
            return HandleResponse(response);
        }

        [HttpGet("filter/status")]
        public async Task<IActionResult> GetByStatus([FromQuery] DonatedItemStatusFilter filter)
        {
            ServiceResponse<IEnumerable<DonatedItemResponseDto>> response;
            switch (filter)
            {
                case DonatedItemStatusFilter.Available:
                    response = await _donatedItemQueryService.GetAvailableDonatedItems();
                    break;

                case DonatedItemStatusFilter.Unavailable:
                    response = await _donatedItemQueryService.GetUnavailableDonatedItems();
                    break;

                case DonatedItemStatusFilter.Deleted:
                    response = await _donatedItemQueryService.GetDeletedDonatedItems();
                    break;

                case DonatedItemStatusFilter.WithoutImages:
                    response = await _donatedItemQueryService.GetDonatedItemsWithoutImages();
                    break;

                case DonatedItemStatusFilter.WithAttachments:
                    response = await _donatedItemQueryService.GetDonatedItemsWithAttachments();
                    break;

                default:
                    response = await _donatedItemService.GetAllDonatedItems(includeDeleted: false);
                    break;
            }

            return HandleResponse(response);
        }

        [HttpGet("filter/status-options")]
        public IActionResult GetStatusOptions()
        {
            var values = Enum.GetValues(typeof(DonatedItemStatusFilter))
                             .Cast<DonatedItemStatusFilter>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString()
                             });

            return Ok(values);
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
