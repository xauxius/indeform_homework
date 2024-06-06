using Microsoft.EntityFrameworkCore;
using storage_api.Models;

namespace storage_api.Services;

public class ImagesService
{
    private readonly FileService _fileService;
    private readonly StorageContext _storageContext;

    public ImagesService(FileService fileService, StorageContext storageContext)
    {
        _fileService = fileService;
        _storageContext = storageContext;
    }

    public List<Image> GetAll()
    {
        var allImages = _storageContext.Images.ToList();
        return allImages;
    }

    public async Task<ImageDTO> CreateImage(string directory, IFormFile image)
    {
        var ext = Path.GetExtension(image.FileName);

        var imagePath = Guid.NewGuid().ToString() + ext;
        var sourcePath =  Path.Combine(directory, imagePath);
        var now = DateTime.Now;
        var imageEntry = new Image {
            Name = image.FileName,
            SourcePath = sourcePath,
            DateAdded = now
        };

        await _fileService.SaveFileAsync(image.OpenReadStream(), sourcePath);

        _storageContext.Images.Add(imageEntry);
        await _storageContext.SaveChangesAsync();

        return new ImageDTO(imageEntry.Id, image.FileName, sourcePath, now.ToString());
    }

    public async Task<Image?> GetImageInfo(int id)
    {
        var imageInfo = await _storageContext.Images.FirstOrDefaultAsync(i => i.Id == id);
        return imageInfo;
    }

    public async Task<bool> DeleteImage(int id)
    {
        var imageInfo = await GetImageInfo(id);

        if (imageInfo == null)
        {
            return false;
        }

        _storageContext.Images.Remove(imageInfo);
        _fileService.DeleteFile(imageInfo.SourcePath);
        await _storageContext.SaveChangesAsync();

        return true;
    }
}