public class ImageDTO
{
    public int Id;
    public string Name;
    public string SourcePath;
    public string DateAdded;

    public ImageDTO(int id, string name, string sourcePath, string dateAdded)
    {
        Id = id;
        Name = name;
        SourcePath = sourcePath;
        DateAdded = dateAdded;
    }
}