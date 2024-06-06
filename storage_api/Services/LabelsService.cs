using Microsoft.EntityFrameworkCore;
using storage_api.Models;

namespace storage_api.Services;

public class LabelsService
{
    private readonly ImagesService _imagesService;
    private readonly StorageContext _storageContext;

    public LabelsService(StorageContext storageContext, ImagesService imagesService)
    {
        _storageContext = storageContext;
        _imagesService = imagesService;
    }

    public async Task<LabelDTO> CreateLabel(CreateLabelDTO createLabelDTO, int imageId)
    {
        var label = new Label {
            ImageId = imageId,
            Class = createLabelDTO.Class.ToString(),
            X = createLabelDTO.X,
            Y = createLabelDTO.Y,
            W = createLabelDTO.W,
            H = createLabelDTO.H
        };

        await _storageContext.AddAsync(label);
        await _storageContext.SaveChangesAsync();
        
        return new LabelDTO(label.Id, label.X, label.Y, label.W, label.H);
    }

    public async Task<List<LabelDTO>> GetLabels(int imageId)
    {
        var labels = await _storageContext.Labels.Where(l => l.ImageId == imageId).ToListAsync();
        var labelsDTO = labels.Select(l => new LabelDTO(l.Id, l.X, l.Y, l.W, l.H)).ToList();
        return labelsDTO;
    }

    public async Task<bool> DeleteLabel(int labelId)
    {
        var label = await _storageContext.Labels.FirstOrDefaultAsync(l => l.Id == labelId);
        if (label != null)
        {
            _storageContext.Labels.Remove(label);
            await _storageContext.SaveChangesAsync();
            return true;
        } else {
            return false;
        }
    }
}