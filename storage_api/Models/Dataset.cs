using System;
using System.Collections.Generic;

namespace storage_api.Models;

public partial class Dataset
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<DatasetEntry> DatasetEntries { get; set; } = new List<DatasetEntry>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
