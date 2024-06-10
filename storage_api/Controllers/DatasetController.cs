using Microsoft.AspNetCore.Mvc;
using storage_api.Services;
using storage_api.Models;

[Route("[controller]")]
public class DatasetController : ControllerBase
{
    private readonly DatasetService _datasetService;

    public DatasetController(DatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    [HttpPost("{name}")]
    public async Task<IActionResult> CreateDataset(string name)
    {
        var dataset = await _datasetService.CreateDataset(name);

        return Ok(new DatasetDTO(dataset.Id, dataset.Name, dataset.CreatedDate));
    }

    [HttpPost("{datasetId}/{setType}/image/{imageId}")]
    public async Task<IActionResult> AddImageToDataset(int datasetId, int imageId, SetType setType)
    {
        await _datasetService.AddImageToDataset(imageId, datasetId, setType);

        return Ok();
    }

    [HttpGet("{datasetId}/{setType}")]
    public async Task<IActionResult> GetDatasetImages(int datasetId, SetType setType)
    {
        var images = await _datasetService.GetDatasetImages(datasetId, setType);

        return Ok(images);
    }
}