namespace Clip.Repository;

public interface IRepository<TObject>
{
    public IEnumerable<TObject> GetCollection();
    public TObject GetObject(string identifier);
    public void InsertObject(TObject obj);
    public void UpdateObject(TObject obj);
    public void DeleteObject(string identifier);
}
