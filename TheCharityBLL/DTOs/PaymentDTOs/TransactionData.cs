using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class TransactionData
    {
        [JsonPropertyName("gateway_integration_pk")]
        public long GatewayIntegrationPk { get; set; }

        [JsonPropertyName("klass")]
        public string? Klass { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }  // Changed to decimal

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("migs_order")]
        public MigsOrder? MigsOrder { get; set; }

        [JsonPropertyName("merchant")]
        public string? Merchant { get; set; }

        [JsonPropertyName("migs_result")]
        public string? MigsResult { get; set; }

        [JsonPropertyName("migs_transaction")]
        public MigsTransaction? MigsTransaction { get; set; }

        [JsonPropertyName("txn_response_code")]
        public string? TxnResponseCode { get; set; }

        [JsonPropertyName("acq_response_code")]
        public string? AcqResponseCode { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("merchant_txn_ref")]
        public string? MerchantTxnRef { get; set; }

        [JsonPropertyName("order_info")]
        public string? OrderInfo { get; set; }

        [JsonPropertyName("receipt_no")]
        public string? ReceiptNo { get; set; }

        [JsonPropertyName("transaction_no")]
        public string? TransactionNo { get; set; }

        [JsonPropertyName("batch_no")]
        public long? BatchNo { get; set; }

        [JsonPropertyName("authorize_id")]
        public string? AuthorizeId { get; set; }

        [JsonPropertyName("card_type")]
        public string? CardType { get; set; }

        [JsonPropertyName("card_num")]
        public string? CardNum { get; set; }

        [JsonPropertyName("secure_hash")]
        public string? SecureHash { get; set; }

        [JsonPropertyName("avs_result_code")]
        public string? AvsResultCode { get; set; }

        [JsonPropertyName("avs_acq_response_code")]
        public string? AvsAcqResponseCode { get; set; }

        [JsonPropertyName("captured_amount")]
        public decimal? CapturedAmount { get; set; }  // Keep as decimal

        [JsonPropertyName("authorised_amount")]
        public decimal? AuthorisedAmount { get; set; }  // Changed to decimal

        [JsonPropertyName("refunded_amount")]
        public decimal? RefundedAmount { get; set; }  // Changed to decimal

        [JsonPropertyName("acs_eci")]
        public string? AcsEci { get; set; }
    }
}