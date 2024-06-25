using System.Text.Json;
using Clip.Data.Configuration;

namespace Clip.Data;

public class JsonDataStore<TData> : IDataStoreAsync<TData>
{
    private readonly DataStoreConfiguration _dataStoreConfiguration;

    public JsonDataStore(DataStoreConfiguration dataStoreConfiguration) => _dataStoreConfiguration = dataStoreConfiguration;

    public async Task<TData?> RetrieveDataAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_dataStoreConfiguration.FilePath))
            return default;
        using var fileStream = File.OpenRead(_dataStoreConfiguration.FilePath);
        return await JsonSerializer.DeserializeAsync<TData>(fileStream, new JsonSerializerOptions(), cancellationToken);
    }

    public Task StoreDataAsync(TData data, CancellationToken cancellationToken)
    {
        var dataAsJson = JsonSerializer.Serialize(data);
        Directory.CreateDirectory(_dataStoreConfiguration.Directory);
        File.WriteAllText(_dataStoreConfiguration.FilePath, dataAsJson);
        return Task.CompletedTask;
    }
}
