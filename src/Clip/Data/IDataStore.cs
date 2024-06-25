namespace Clip.Data;

public interface IDataStoreAsync<TData>
{
    public Task<TData?> RetrieveDataAsync(CancellationToken cancellationToken);
    public Task StoreDataAsync(TData data, CancellationToken cancellationToken);
}
