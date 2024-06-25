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
    public async Task UserDataDoesntExistIfNotPreviouslyStored()
    {
        var userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is null);
    }
    
    [Test]
    public async Task AddOneObjectAsync()
    {
        var testUserId = Guid.NewGuid().ToString(); 
        var user = new User()
        {
            Id = testUserId,
            Name = "Test User",
            Email = "test.user@email.com",
            CreationTime = DateTime.UtcNow
        };
        var userData = new UserData();
        userData.Users.Add(user);
        await _userDataDataStore.StoreDataAsync(userData, _cancellationToken);
        userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is not null);
        Assert.That(userData.Users.Count() == 1);
        Assert.That(userData.Users.All(x => x.Id == testUserId));
    }
    
    [Test]
    public async Task AddMultipleObjectsAsync()
    {
        var users = CreateUsers(10);
        var userData = new UserData
        {
            Users = users
        };
        await _userDataDataStore.StoreDataAsync(userData, _cancellationToken);
        userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is not null);
        Assert.That(userData.Users.Count() == 10);
        for (var i = 0; i < 10; i++)
            Assert.That(userData.Users.Where(user => user.Id == i.ToString()).Count() == 1);
    }

    [Test]
    public async Task RemoveOneObjectAsync()
    {
        var users = CreateUsers(10);
        var userData = new UserData
        {
            Users = users
        };
        await _userDataDataStore.StoreDataAsync(userData, _cancellationToken);
        userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        userData.Users.RemoveAt(0);
        await _userDataDataStore.StoreDataAsync(userData, _cancellationToken);
        userData = await _userDataDataStore.RetrieveDataAsync(_cancellationToken);
        Assert.That(userData is not null);
        Assert.That(userData.Users.Count() == 9);
        for (var i = 1; i < 10; i++)
            Assert.That(userData.Users.Where(user => user.Id == i.ToString()).Count() == 1);
        Assert.That(userData.Users.Where(user => user.Id == "0").Count() == 0);
    }

    public List<User> CreateUsers(int amount)
    {
        var users = new List<User>();
        for (var i = 0; i < amount; i++)
        {
            var user = new User()
            {
                Id = i.ToString(),
                Name = $"User {i}",
                Email = $"user{i}@email.com",
                CreationTime = DateTime.UtcNow
            };
            users.Add(user);
        }
        return users;
    }
}
