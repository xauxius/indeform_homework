public class ImageDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SourcePath { get; set; }
    public string DateAdded { get; set; }

    public ImageDTO(int id, string name, string sourcePath, string dateAdded)
    {
        Id = id;
        Name = name;
        SourcePath = sourcePath;
        DateAdded = dateAdded;
    }
}