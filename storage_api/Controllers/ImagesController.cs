using Microsoft.AspNetCore.Mvc;
using storage_api.Services;

[Route("[Controller]")]
public class ImagesController : ControllerBase
{
    private readonly ImagesService _imagesService;
    private readonly FileService _fileService;
    private readonly string _directory = Constants.IMAGES_PATH; 

    public ImagesController(ImagesService imagesService, FileService fileService)
    {
        _imagesService = imagesService;
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        var imageDto = await _imagesService.CreateImage(_directory, image);
        
        return Ok(imageDto);
    }

    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImageInfo(int imageId)
    {
        var imageInfo = await _imagesService.GetImageInfo(imageId);
        if (imageInfo != null)
        {
            var imageDTO = new ImageDTO(imageInfo.Id, imageInfo.Name, imageInfo.SourcePath, imageInfo.DateAdded.ToString());
            return Ok(imageDTO);
        } else {
            return NotFound();
        }
    }

    [HttpGet("download/{imageId}")]
    public async Task<IActionResult> DownloadImage(int imageId)
    {
        var imageInfo = await _imagesService.GetImageInfo(imageId);
        if (imageInfo == null)
            return NotFound();
        var filePath = imageInfo.SourcePath;
        var stream = await _fileService.GetFileAsync(filePath);

        return File(stream, "application/octet-stream");
    }


    [HttpDelete("{imageId}")]
    public async Task<IActionResult> DeleteImage(int imageId)
    {
        var success = await _imagesService.DeleteImage(imageId);

        if (success)
        {
            return Ok();
        } else {
            return NotFound();
        }
    }
}