using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonitorDataAccess.Config;
using Domain.DTOs;
using Newtonsoft.Json;

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

    public async Task Add(StatsEntry entry)
    {
        var fullPath = Path.GetFullPath(localFileData.Value.MoodData);
        var moodEntries = await GetAll();
        moodEntries.Add(entry);
        var json = JsonConvert.SerializeObject(moodEntries);
        await File.WriteAllTextAsync(fullPath, json);
    }

    public async Task<List<StatsEntry>> GetAll()
    {
        var fullPath = Path.GetFullPath(localFileData.Value.MoodData);

        if (!File.Exists(fullPath))
        {
            _logger.LogWarning("Local mood data file not present : {path}", fullPath);
            var list = new List<StatsEntry>()
            {
                new StatsEntry()
                {
                    Creation = DateTime.Now,
                }
            };

            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fullPath, json);
        }

        var text = File.ReadAllText(fullPath);
        return await Task.FromResult(JsonConvert.DeserializeObject<List<StatsEntry>>(text));

    }


}
