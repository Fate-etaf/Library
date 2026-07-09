using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public int? YearPublish { get; set; }

    public int? Quantity { get; set; }

    public int? CategoryId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    public virtual Category? Category { get; set; }
}
