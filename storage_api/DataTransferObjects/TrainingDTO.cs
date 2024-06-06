public class TrainingDTO
{
    public int? StartModelId;
    public int? EndModelId;
    public string StartDate;
    public string? EndDate;
    public int? EpochCount;
    public float? LearningRate;
    public bool FinishedTraining;

    public TrainingDTO(int? startModelId, int? endModelId, string startDate, string? endDate, int? epochCount, float? learningRate, bool finishedTraining)
    {
        StartModelId = startModelId;
        EndModelId = endModelId;
        StartDate = startDate;
        EndDate = endDate;
        EpochCount = epochCount;
        LearningRate = learningRate;
        FinishedTraining = finishedTraining;
    }
}