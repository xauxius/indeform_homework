using System;
using System.Collections.Generic;

namespace storage_api.Models;

public partial class DatasetEntry
{
    public int DatasetId { get; set; }

    public int ImageId { get; set; }

    public string SetType { get; set; } = null!;

    public virtual Dataset Dataset { get; set; } = null!;

    public virtual Image Image { get; set; } = null!;
}
