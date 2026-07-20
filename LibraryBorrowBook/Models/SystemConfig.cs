using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class SystemConfig
{
    public int ConfigId { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string ConfigValue { get; set; } = null!;

    public string? Description { get; set; }
}
