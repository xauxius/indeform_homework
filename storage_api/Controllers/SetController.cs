using Microsoft.AspNetCore.Mvc;
using storage_api.Services;
using storage_api.Models;

[Route("[controller]")]
public class SetController : ControllerBase
{
    private readonly DatasetService _setService;

    public SetController(DatasetService setService)
    {
        _setService = setService;
    }

    [HttpGet("{datasetId}/{setType}")]
    public async Task<IActionResult> GetSetImages(int datasetId, SetType setType)
    {
        var set = await _setService.GetDatasetImages(datasetId, setType);

        return Ok(set);
    }

}