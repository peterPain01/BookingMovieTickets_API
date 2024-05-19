using System;
using System.Collections.Generic;

namespace MovieTickets.Api.EntityModels;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? VoucherCode { get; set; }

    public string? VoucherType { get; set; }

    public decimal? DiscountValue { get; set; }

    public DateOnly? ValidFrom { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public int? UsageLimit { get; set; }

    public int? RemainingUsage { get; set; }
}
