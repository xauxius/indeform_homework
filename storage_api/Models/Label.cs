using System;
using System.Collections.Generic;

namespace storage_api.Models;

public partial class Label
{
    public int Id { get; set; }

    public int ImageId { get; set; }

    public string Class { get; set; } = null!;

    public float X { get; set; }

    public float Y { get; set; }

    public float W { get; set; }

    public float H { get; set; }

    public virtual Image Image { get; set; } = null!;
}
