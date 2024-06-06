using System;
using System.Collections.Generic;

namespace storage_api.Models;

public partial class Model
{
    public int Id { get; set; }

    public int? TrainDatasetId { get; set; }

    public int? StartModelId { get; set; }

    public string Name { get; set; } = null!;

    public string SourcePath { get; set; } = null!;

    public virtual ICollection<Model> InverseStartModel { get; set; } = new List<Model>();

    public virtual Model? StartModel { get; set; }

    public virtual Dataset? TrainDataset { get; set; }
}
