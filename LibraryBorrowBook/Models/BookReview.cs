using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class BookReview
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime ReviewDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
