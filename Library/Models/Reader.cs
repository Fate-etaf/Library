using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string ReaderName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
}
