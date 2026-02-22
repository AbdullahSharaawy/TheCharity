
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class OrganizationQueryService : IOrganizationQueryService
    {
        private readonly IOrganizationRepository _repository;
        public OrganizationQueryService(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public Task<int> GetActiveOrganizationsCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetByCampaignCount(int minCampaigns)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetDeletedOrganizations()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetRecentlyRegisteredOrganizations(int days)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalOrganizationsCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetWithActiveCampaigns()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetWithCompletedCampaigns()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetWithoutCampaigns()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetWithoutPaymentInfo()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> SearchOrganizations(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
