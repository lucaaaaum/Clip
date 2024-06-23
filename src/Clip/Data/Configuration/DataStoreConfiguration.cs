namespace Clip.Data.Configuration;

public class DataStoreConfiguration
{
    public string Directory { get; set; }
    public string Name { get; set; }
    public DataFormat Format { get; set; }
    public string FilePath => Path.Combine(Directory, $"{Name}.{Format}");
}
