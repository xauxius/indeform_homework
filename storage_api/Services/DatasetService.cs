using System.Linq;
using Microsoft.EntityFrameworkCore;
using storage_api.Models;

namespace storage_api.Services;

public class DatasetService
{
    private readonly StorageContext _storageContext;

    public DatasetService(StorageContext storageContext)
    {
        _storageContext = storageContext;
    }

    public async Task<Dataset> CreateDataset(string name)
    {
        var dataset = new Dataset {
            Name = name,
            CreatedDate = DateTime.Now
        };

        _storageContext.Datasets.Add(dataset);
        await _storageContext.SaveChangesAsync();

        return dataset;
    }

    public async Task AddImageToDataset(int imageId, int datasetId, SetType setType)
    {
        var datasetEntry = new DatasetEntry {
            DatasetId = datasetId,
            ImageId = imageId,
            SetType = setType.ToString()
        };

        _storageContext.DatasetEntries.Add(datasetEntry);
        await _storageContext.SaveChangesAsync();
    }

    public async Task<List<int>> GetDatasetImages(int datasetId, SetType setType)
    {
        var set = await _storageContext.DatasetEntries
            .Where(ds => ds.DatasetId == datasetId && ds.SetType == setType.ToString())
            .Select(ds => ds.ImageId).ToListAsync();
        
        return set;
    }
}