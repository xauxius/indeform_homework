public class DatasetDTO
{
    public int Id;
    public string Name;
    public DateTime CreatedDate;

    public DatasetDTO(int id, string name, DateTime createdDate)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
    }
}