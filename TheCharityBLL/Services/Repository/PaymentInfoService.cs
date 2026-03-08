using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.PaymentInfoDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Entities;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class PaymentInfoService : IPaymentInfoService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentInfoService> _logger;

        public PaymentInfoService(
            IOrganizationRepository organizationRepository,
            IMapper mapper,
            ILogger<PaymentInfoService> logger)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // ===== Core CRUD =====

        public async Task<PaymentInfoResponseDto?> GetPaymentInfoByOrganizationIdAsync(int organizationId)
        {
            _logger.LogInformation("Fetching payment info for organization ID {OrganizationId}.", organizationId);

            var organizationExists = await _organizationRepository.OrganizationExistsAsync(organizationId);
            if (!organizationExists)
            {
                _logger.LogWarning("Organization with ID {OrganizationId} was not found.", organizationId);
                throw new KeyNotFoundException($"Organization with ID {organizationId} was not found.");
            }

            var paymentInfo = await _organizationRepository.GetPaymentInfoByOrganizationIdAsync(organizationId);
            if (paymentInfo == null)
            {
                _logger.LogInformation("No payment info found for organization ID {OrganizationId}.", organizationId);
                return null;
            }

            return _mapper.Map<PaymentInfoResponseDto>(paymentInfo);
        }

        public async Task<PaymentInfoResponseDto?> GetPaymentInfoByIdAsync(int paymentInfoId)
        {
            _logger.LogInformation("Fetching payment info with ID {PaymentInfoId}.", paymentInfoId);

            var paymentInfo = await _organizationRepository.GetPaymentInfoByIdAsync(paymentInfoId);
            if (paymentInfo == null)
            {
                _logger.LogWarning("PaymentInfo with ID {PaymentInfoId} was not found.", paymentInfoId);
                throw new KeyNotFoundException($"PaymentInfo with ID {paymentInfoId} was not found.");
            }

            return _mapper.Map<PaymentInfoResponseDto>(paymentInfo);
        }

        public async Task<PaymentInfoResponseDto> CreatePaymentInfoAsync( CreatePaymentInfoDto dto)
        {
           
            var paymentInfo = _mapper.Map<PaymentInfo>(dto);
            var created = await _organizationRepository.AddPaymentInfoAsync(paymentInfo);

            _logger.LogInformation("Payment info created with ID {PaymentInfoId} .",
                created.Id);

            return _mapper.Map<PaymentInfoResponseDto>(created);
        }

        public async Task<PaymentInfoResponseDto> UpdatePaymentInfoAsync(int paymentInfoId, UpdatePaymentInfoDto dto)
        {
            _logger.LogInformation("Updating payment info with ID {PaymentInfoId}.", paymentInfoId);

            var paymentInfo = await _organizationRepository.GetPaymentInfoByIdAsync(paymentInfoId);
            if (paymentInfo == null)
            {
                _logger.LogWarning("PaymentInfo with ID {PaymentInfoId} was not found.", paymentInfoId);
                throw new KeyNotFoundException($"PaymentInfo with ID {paymentInfoId} was not found.");
            }

            if (paymentInfo.IsDeleted)
            {
                _logger.LogWarning("PaymentInfo with ID {PaymentInfoId} is deleted and cannot be updated.", paymentInfoId);
                throw new InvalidOperationException($"PaymentInfo with ID {paymentInfoId} is deleted and cannot be updated.");
            }

            if (!string.IsNullOrWhiteSpace(dto.ApiKey))
                paymentInfo.EditApiKey(dto.ApiKey);

            if (!string.IsNullOrWhiteSpace(dto.IntegrationId))
                paymentInfo.EditIntegrationId(dto.IntegrationId);

            if (!string.IsNullOrWhiteSpace(dto.IframeId))
                paymentInfo.EditIframeId(dto.IframeId);

            if (!string.IsNullOrWhiteSpace(dto.HmacKey))
                paymentInfo.EditHmacKey(dto.HmacKey);

            var updated = await _organizationRepository.UpdatePaymentInfoAsync(paymentInfo);

            _logger.LogInformation("Payment info with ID {PaymentInfoId} updated successfully.", paymentInfoId);

            return _mapper.Map<PaymentInfoResponseDto>(updated);
        }

        public async Task DeletePaymentInfoAsync(int paymentInfoId)
        {
            _logger.LogInformation("Deleting payment info with ID {PaymentInfoId}.", paymentInfoId);

            var paymentInfo = await _organizationRepository.GetPaymentInfoByIdAsync(paymentInfoId);
            if (paymentInfo == null)
            {
                _logger.LogWarning("PaymentInfo with ID {PaymentInfoId} was not found.", paymentInfoId);
                throw new KeyNotFoundException($"PaymentInfo with ID {paymentInfoId} was not found.");
            }

            if (paymentInfo.IsDeleted)
            {
                _logger.LogWarning("PaymentInfo with ID {PaymentInfoId} is already deleted.", paymentInfoId);
                throw new InvalidOperationException($"PaymentInfo with ID {paymentInfoId} is already deleted.");
            }

            await _organizationRepository.DeletePaymentInfoAsync(paymentInfoId);

            _logger.LogInformation("Payment info with ID {PaymentInfoId} deleted successfully.", paymentInfoId);
        }

        // PaymentInfoService.cs
        public async Task RestorePaymentInfoAsync(int paymentInfoId)
        {
            _logger.LogInformation("Restoring payment info with ID {PaymentInfoId}.", paymentInfoId);

             var exists = await _organizationRepository.OrganizationExistsAsync(paymentInfoId); // not useful here

            await _organizationRepository.RestorePaymentInfoAsync(paymentInfoId);

            _logger.LogInformation("Payment info with ID {PaymentInfoId} restored successfully.", paymentInfoId);
        }

        // ===== Utilities =====

        public async Task<bool> HasPaymentInfoAsync(int organizationId)
        {
            _logger.LogInformation("Checking if organization ID {OrganizationId} has payment info.", organizationId);

            var organizationExists = await _organizationRepository.OrganizationExistsAsync(organizationId);
            if (!organizationExists)
            {
                _logger.LogWarning("Organization with ID {OrganizationId} was not found.", organizationId);
                throw new KeyNotFoundException($"Organization with ID {organizationId} was not found.");
            }

            return await _organizationRepository.HasPaymentInfoAsync(organizationId);
        }

        public async Task<bool> ValidatePaymentInfoAsync(int organizationId)
        {
            _logger.LogInformation("Validating payment info for organization ID {OrganizationId}.", organizationId);

            var organizationExists = await _organizationRepository.OrganizationExistsAsync(organizationId);
            if (!organizationExists)
            {
                _logger.LogWarning("Organization with ID {OrganizationId} was not found.", organizationId);
                throw new KeyNotFoundException($"Organization with ID {organizationId} was not found.");
            }

            var isValid = await _organizationRepository.ValidatePaymentInfoAsync(organizationId);

            _logger.LogInformation("Payment info validation result for organization ID {OrganizationId}: {IsValid}.",
                organizationId, isValid);

            return isValid;
        }
    }
}
