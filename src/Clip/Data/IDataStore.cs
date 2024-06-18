namespace Clip.Data;

public interface IDataStore
{
    public void GetCollection<TObject>();
    public void GetObject<TObject>(string identifier);
    public void InsertObject<TObject>(TObject obj);
    public void UpdateObject<TObject>(TObject obj);
    public void DeleteObject<TObject>(TObject obj);
}
