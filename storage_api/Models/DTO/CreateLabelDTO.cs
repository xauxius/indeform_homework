using System.Text.Json.Serialization;
using storage_api.Models;

public class CreateLabelDTO
{
    public DetectionClass Class { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
}