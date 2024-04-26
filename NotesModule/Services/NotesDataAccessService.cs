using MonitorDataAccess.DataAccess;
using Domain.DTOs;
using NotesModule.Entities;
using static NotesModule.Entities.LogMappingProfile;

namespace NotesModule.Services;

[AutoConstructor]
public partial class NotesDataAccessService
{
    private readonly FileLogDataAccess _fileDataAccess;
    private readonly ImportFromGoogleNotesDataAccess _fileImportDataAccess;
    private readonly DBLogDataAccess _dbLogDataAccess;

    public async Task<List<LogEntry>> GetAll()
    {
        var dbList = await _dbLogDataAccess.GetAll();
        return dbList;

        var fileList = await _fileDataAccess.GetAll();
        var importList = await _fileImportDataAccess.GetAll();

        var mappedImportList = importList.Select(s => s.Map());

        return Enumerable.Empty<LogEntry>()
                         .Concat(fileList)
                         .Concat(mappedImportList)
                         .ToList();
    }

    public async Task<LogEntry> Add(LogEntry logEntry)
    {
        return await _dbLogDataAccess.Add(logEntry);
    }
}
