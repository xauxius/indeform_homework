using Microsoft.AspNetCore.Mvc;
using storage_api.Models;
using storage_api.Services;

[Route("[controller]")]
public class ModelsController : ControllerBase
{
    private readonly ModelService _modelService;
    private readonly string _directory;

    public ModelsController(ModelService modelService)
    {
        _modelService = modelService;
        _directory = Constants.MODELS_PATH;
    }

    [HttpPost]
    public async Task<IActionResult> UploadModel(IFormFile modelFile, CreateModelDTO createModelDTO)
    {
        var model = await _modelService.UploadModel(createModelDTO, modelFile, _directory);

        return Ok(MapToModelDTO(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllModelInfo()
    {
        var models = await _modelService.GetAllModels();
        var modelsDTO = models.Select(m => MapToModelDTO(m));
        
        return Ok(modelsDTO);
    }

    [HttpGet("download/{modelId}")]
    public async Task<IActionResult> DownloadModel(int modelId)
    {
        var modelFile = await _modelService.DownloadModel(modelId);
        if (modelFile == null)
            return NotFound();

        return File(modelFile, "application/octet-stream");
    }

    private ModelDTO MapToModelDTO(Model model)
    {
        return new ModelDTO(model.Id, model.TrainDatasetId, model.StartModelId, model.Name, model.SourcePath);
    }
}