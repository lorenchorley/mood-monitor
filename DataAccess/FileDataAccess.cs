using DataAccess.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DataAccess;

public class FileDataAccess : IDataAccess
{
    //private readonly string _filePath;
    private readonly IOptions<LocalFileData> localFileData;
    private readonly ILogger<FileDataAccess> _logger;

    public FileDataAccess(IOptions<LocalFileData> configuration, ILogger<FileDataAccess> logger)
    {
        localFileData = configuration;
        _logger = logger;
    }

    public async Task<List<EnergyEntry>> GetAllEnergyEntries()
    {
        throw new NotImplementedException();
    }

    public async Task<List<LogEntry>> GetAllLogEntries()
    {
        throw new NotImplementedException();
    }

    public async Task<List<MoodEntry>> GetAllMoodEntries()
    {
        var fullPath = Path.GetFullPath(localFileData.Value.MoodData);

        if (!File.Exists(fullPath))
        {
            _logger.LogWarning("Local mood data file not present : {path}", fullPath);
            var list = new List<MoodEntry>()
            {
                new MoodEntry(DateTime.Now, 1)
            };

            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fullPath, json);
        }

        var text = File.ReadAllText(fullPath);
        return JsonConvert.DeserializeObject<List<MoodEntry>>(text);

    }
}
