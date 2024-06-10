public class ModelDTO
{
    public int ID { get; set; }
    public int? TrainingDatasetID { get; set; }
    public int? StartModelID { get; set; }
    public string Name { get; set; }
    public string SourcePath { get; set;}

    public ModelDTO(int id, int? trainingDatasetId, int? startModelId, string name, string sourcePath)
    {
        ID = id;
        TrainingDatasetID = trainingDatasetId;
        StartModelID = startModelId;
        Name = name;
        SourcePath = sourcePath;
    }
}