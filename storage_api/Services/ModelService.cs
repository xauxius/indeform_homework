using Microsoft.EntityFrameworkCore;
using storage_api.Models;

namespace storage_api.Services;

public class ModelService
{
    private readonly FileService _fileService;
    private readonly StorageContext _storageContext;

    public ModelService(FileService fileService, StorageContext storageContext)
    {
        _fileService = fileService;
        _storageContext = storageContext;
    }

    public async Task<Model> UploadModel(CreateModelDTO createModelDTO, IFormFile modelFile, string directory)
    {
        var ext = Path.GetExtension(modelFile.FileName);
        var filePath = Guid.NewGuid().ToString() + ext;
        var sourcePath =  Path.Combine(directory, filePath);

        await _fileService.SaveFileAsync(modelFile.OpenReadStream(), sourcePath);

        var model = new Model {
            TrainDatasetId = createModelDTO.TrainingDatasetID,
            StartModelId = createModelDTO.StartModelID,
            Name = createModelDTO.Name,
            SourcePath = sourcePath
        };

        _storageContext.Models.Add(model);
        await _storageContext.SaveChangesAsync();

        return model;
    }

    public async Task<List<Model>> GetAllModels()
    {
        var models = await _storageContext.Models.ToListAsync();

        return models;
    }

    public async Task<Model?> GetModel(int modelId)
    {
        var model = await _storageContext.Models.FirstOrDefaultAsync(m => m.Id == modelId);

        return model;
    }

    public async Task<Stream?> DownloadModel(int modelId)
    {
        var model = await GetModel(modelId);
        if (model == null)
            return null;
        
        var modelFile = await _fileService.GetFileAsync(model.SourcePath);

        return modelFile;
    }
}