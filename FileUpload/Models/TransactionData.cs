using System;
using System.Collections.Generic;

namespace FileUploadAPI.Models;

public partial class TransactionData
{
    public long Id { get; set; }

    public string TransactionId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public DateTime TransactionDatetime { get; set; }

    public string Status { get; set; } = null!;
}
