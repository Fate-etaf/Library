using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Fine
{
    public int FineId { get; set; }

    public int BorrowId { get; set; }

    public decimal Amount { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly? PaidDate { get; set; }

    public virtual Borrow Borrow { get; set; } = null!;
}
