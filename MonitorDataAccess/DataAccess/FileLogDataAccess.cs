using Domain.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonitorDataAccess.Config;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace MonitorDataAccess.DataAccess;

public class FileLogDataAccess : IDataAccess<LogEntry>
{
    //private readonly string _filePath;
    private readonly IOptions<LocalFileData> localFileData;
    private readonly ILogger<FileLogDataAccess> _logger;

    public FileLogDataAccess(IOptions<LocalFileData> configuration, ILogger<FileLogDataAccess> logger)
    {
        localFileData = configuration;
        _logger = logger;
    }

    public async Task<LogEntry> Add(LogEntry entry)
    {
        var fullPath = Path.GetFullPath(localFileData.Value.LogData);
        var logEntries = await GetAll();
        logEntries.Add(entry);
        logEntries = logEntries.Select(e => e.UpdateEncryptedText()).ToList();
        var json = JsonConvert.SerializeObject(logEntries);
        await File.WriteAllTextAsync(fullPath, json);

        return entry;
    }

    public async Task<List<LogEntry>> GetAll()
    {
        var fullPath = Path.GetFullPath(localFileData.Value.LogData);

        if (!File.Exists(fullPath))
        {
            _logger.LogWarning("Local log data file not present : {path}", fullPath);
            var list = new List<LogEntry>()
            {
                new LogEntry()
                {
                    Creation = DateOnly.FromDateTime(DateTime.Now),
                    Text = "test entry"
                }.UpdateDecryptedText()
            };

            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fullPath, json);
        }

        var text = File.ReadAllText(fullPath);
        List<LogEntry>? logEntries = JsonConvert.DeserializeObject<List<LogEntry>>(text);
        logEntries = logEntries.Select(e => e.UpdateDecryptedText()).ToList();

        return await Task.FromResult(logEntries);
    }

}
