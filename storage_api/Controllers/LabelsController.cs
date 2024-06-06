using Microsoft.AspNetCore.Mvc;
using storage_api.Services;

[Route("[Controller]")]
public class LabelsController : ControllerBase
{
    private readonly LabelsService _labelsService;

    public LabelsController(LabelsService labelsService)
    {
        _labelsService = labelsService;
    }

    [HttpPost("{imageId}")]
    public async Task<IActionResult> CreateLabel(int imageId, CreateLabelDTO createLabelDTO)
    {
        var labelDTO = await _labelsService.CreateLabel(createLabelDTO, imageId);
        return Ok(labelDTO);
    }

    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetLabelsByImage(int imageId)
    {
        var labelsDTO = await _labelsService.GetLabels(imageId);
        return Ok(labelsDTO);
    }

    [HttpDelete("{labelId}")]
    public async Task<IActionResult> DeleteLabel(int labelId)
    {
        var success = await _labelsService.DeleteLabel(labelId);
        if (success)
        {
            return Ok();
        } else {
            return NotFound();
        }
    }
}