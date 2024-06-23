namespace Clip.Data.Configuration;

public class DataStoreConfiguration
{
    public string Directory { get; set; }
    public string Name { get; set; }
    public DataStoreType Type { get; set; }
    public string FilePath => Path.Combine(Directory, $"{Name}.{Type}");
}
