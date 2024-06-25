using System.Text.Json;
using Clip.Data;
using Clip.Data.Configuration;
using Clip.Test.TestInfrastructure;

namespace Clip.Test.Data;

[TestFixture]
public class JsonDataStoreTest
{
    private IDataStoreAsync<UserData> _userDataDataStore;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken => _cancellationTokenSource.Token;

    [SetUp]
    public void SetUp()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        var config = new DataStoreConfiguration()
        {
            Directory = "./data",
            Name = "JsonDataStoreTest",
            Format = DataFormat.JSON
        };
        _userDataDataStore = new JsonDataStore<UserData>(config);
    }

    [TearDown]
    public void TearDown()
    {
        var filePath = "./data/JsonDataStoreTest.JSON";
        if (File.Exists(filePath))
            File.Delete(filePath);
        _cancellationTokenSource.Dispose();
    }

    [Test]
    public async Task AddNewObjectAsync()
    {
        var userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is null);
        var testUserId = Guid.NewGuid().ToString(); 
        var user = new User()
        {
            Id = testUserId,
            Name = "Test User",
            Email = "test.user@email.com",
            CreationTime = DateTime.UtcNow
        };
        userData = new UserData();
        userData.Users.Add(user);
        await _userDataDataStore.StoreDataAsync(userData, _cancellationToken);
        userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is not null);
        Assert.That(userData.Users.Count() == 1);
        Assert.That(userData.Users.All(x => x.Id == testUserId));
    }
}
