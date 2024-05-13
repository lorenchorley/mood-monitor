using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonitorDataAccess.Config;
using Domain.DTOs;
using Newtonsoft.Json;
using static MonitorDataAccess.Extensions.MoodEntryExtensions;

namespace MonitorDataAccess.DataAccess;

public class FileMoodDataAccess : IDataAccess<StatsEntry>
{
    //private readonly string _filePath;
    private readonly IOptions<LocalFileData> localFileData;
    private readonly ILogger<FileMoodDataAccess> _logger;

    public FileMoodDataAccess(IOptions<LocalFileData> configuration, ILogger<FileMoodDataAccess> logger)
    {
        localFileData = configuration;
        _logger = logger;
    }

    public async Task<StatsEntry> Add(StatsEntry entry)
    {
        var fullPath = Path.GetFullPath(localFileData.Value.MoodData);

        var moodEntries = await GetAll();
        moodEntries.Add(entry);

        var json = moodEntries.SerialiseToList();
        await File.WriteAllTextAsync(fullPath, json);

        return entry;
    }

    public async Task<List<StatsEntry>> GetAll()
    {
        var fullPath = Path.GetFullPath(localFileData.Value.MoodData);

        if (!File.Exists(fullPath))
        {
            _logger.LogWarning("Local mood data file not present : {path}", fullPath);
            File.WriteAllText(fullPath, JsonConvert.SerializeObject(new List<StatsEntry>()));
        }

        var json = File.ReadAllText(fullPath);
        return await Task.FromResult(json.DeserialiseToList());

    }

}
