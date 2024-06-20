namespace Clip.Data;

public interface IDataStore
{
    public IEnumerable<TObject> GetCollection<TObject>();
    public TObject GetObject<TObject>(string identifier);
    public void InsertObject<TObject>(TObject obj);
    public void UpdateObject<TObject>(TObject obj);
    public void DeleteObject<TObject>(string identifier);
}
