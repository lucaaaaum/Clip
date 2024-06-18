using System.Text.Json;
using Clip.Data.Configuration;

namespace Clip.Data;

public class JsonDataStore : IDataStore
{
    private readonly DataStoreConfiguration _dataStoreConfiguration;
    private string FilePath { get; set; }

    public JsonDataStore(DataStoreConfiguration dataStoreConfiguration)
    {
        _dataStoreConfiguration = dataStoreConfiguration;
        FilePath = 
            $"{_dataStoreConfiguration.Directory}/" +
            $"{_dataStoreConfiguration.Name}." +
            $"{_dataStoreConfiguration.Type.ToString().ToLowerInvariant()}";
    }

    public IEnumerable<TObject> GetCollection<TObject>()
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, List<object>>>(FilePath);
        var collectionName = typeof(TObject).Name;
        var collectionExists = data.TryGetValue(collectionName, out var collection);
        return collectionExists && collection is not null ? (IEnumerable<TObject>) collection : [];
    }

    public TObject GetObject<TObject>(string identifier)
    {
        throw new NotImplementedException();
    }

    public void InsertObject<TObject>(TObject obj)
    {
        throw new NotImplementedException();
    }

    public void UpdateObject<TObject>(TObject obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteObject<TObject>(TObject obj)
    {
        throw new NotImplementedException();
    }
}
