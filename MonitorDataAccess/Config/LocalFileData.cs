namespace MonitorDataAccess.Config;

public class LocalFileData
{
    internal const string ConfigurationSection = "LocalFileData";

    public string MoodData { get; set; } = null!;
    public string LogData { get; set; } = null!;
}
