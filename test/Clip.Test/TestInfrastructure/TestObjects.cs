namespace Clip.Test.TestInfrastructure;

public class User
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public DateTime CreationTime { get; init; }
}

public class UserData
{
    public List<User> Users { get; init; } = [];
}
