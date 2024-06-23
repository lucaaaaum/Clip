using System.Text.Json;
using Clip.Data.Configuration;

namespace Clip.Data;

public class JsonDataStore : IDataStore
{
    private readonly DataStoreConfiguration _dataStoreConfiguration;

    public JsonDataStore(DataStoreConfiguration dataStoreConfiguration) => _dataStoreConfiguration = dataStoreConfiguration;

    public IEnumerable<TObject> GetCollection<TObject>()
    {
        var file = ReadFile();
        var data = JsonSerializer.Deserialize<Dictionary<string, List<object>>>(file);
        var collectionName = typeof(TObject).Name;
        var collectionExists = data.TryGetValue(collectionName, out var collection);
        return collectionExists && collection is not null ? (IEnumerable<TObject>)collection : [];
    }

    private byte[] ReadFile()
    {
        using var file = File.OpenRead(_dataStoreConfiguration.FilePath);
        var buffer = new byte[file.Length];
        file.Read(buffer);
        return buffer;
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

    public void DeleteObject<TObject>(string identifier)
    {
        throw new NotImplementedException();
    }
}
