namespace storage_api.Models;

public class LabelDTO
{
    public int Id { get; set; }
    public DetectionClass Class { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }

    public LabelDTO(int id, float x, float y, float w, float h)
    {
        Id = id;
        X = x;
        Y = y;
        W = w;
        H = h;
    }
}