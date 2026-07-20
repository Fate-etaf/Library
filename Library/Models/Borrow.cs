using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Borrow
{
    public int BorrowId { get; set; }

    public int ReaderId { get; set; }

    public int BookId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
