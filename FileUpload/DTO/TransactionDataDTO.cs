namespace FileUploadAPI.DTO
{
    public class TransactionDataDTO
    {
        public string TransactionId { get; set; } = null!;

        public string Amount { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;

        public DateTime TransactionDatetime { get; set; }

        public string Status { get; set; } = null!;
    }
}
