namespace storage_api.Services;

public class FileService
{
    public async Task<string> SaveFileAsync(Stream fileStream, string filePath)
    {
        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await fileStream.CopyToAsync(file);
        }
        return filePath;
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        var memoryStream = new MemoryStream();
        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            await file.CopyToAsync(memoryStream);
        }
        memoryStream.Position = 0;
        return memoryStream;
    }

    public bool DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}