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

    public async Task<List<LogEntry>> GetAll()
    {
        var fileList = await _fileDataAccess.GetAll();
        var importList = await _fileImportDataAccess.GetAll();

        var mappedFileList = fileList;
        var mappedImportList = importList.Select(s => s.Map());

        return mappedFileList
            .Concat(mappedImportList).ToList();
    }

    public async Task Add(LogEntry logEntry)
    {
        await _fileDataAccess.Add(logEntry);
    }
}
