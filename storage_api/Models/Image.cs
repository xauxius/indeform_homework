using System;
using System.Collections.Generic;

namespace storage_api.Models;

public partial class Image
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string SourcePath { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public virtual ICollection<DatasetEntry> DatasetEntries { get; set; } = new List<DatasetEntry>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
}
