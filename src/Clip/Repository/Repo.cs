
using Clip.Data;

namespace Clip.Repository;

public class Repo<TObject> : IRepository<TObject>
{
    private readonly IDataStore _dataStore;

    public Repo(IDataStore dataStore) => _dataStore = dataStore;

    public IEnumerable<TObject> GetCollection() => _dataStore.GetCollection<TObject>();

    public TObject Get(string identifier) => _dataStore.GetObject<TObject>(identifier);

    public void Insert(TObject obj) => _dataStore.InsertObject<TObject>(obj);

    public void Update(TObject obj) => _dataStore.UpdateObject<TObject>(obj);

    public void Delete(string identifier) => _dataStore.DeleteObject<TObject>(identifier);
}
