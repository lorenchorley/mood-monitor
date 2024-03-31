using MonitorDataAccess.DataAccess;
using Domain.DTOs;
using MoodModule.Entities;
using System.Linq;

namespace MoodModule.Services;

[AutoConstructor]
public partial class MoodDataAccessService
{
    private readonly FileMoodDataAccess _fileDataAccess;
    private readonly ImportFromMoodHistoryDataAccess _fileImportDataAccess;

    public async Task<List<StatsEntry>> GetAll()
    {
        var fileList = await _fileDataAccess.GetAll();
        var importList = await _fileImportDataAccess.GetAll();

        var mappedFileList = fileList;
        var mappedImportList = importList.Select(s => s.Map());

        return mappedFileList.Concat(mappedImportList).ToList();
    }

    public async Task Add(StatsEntry statsEntry)
    {
        await _fileDataAccess.Add(statsEntry);
    }

}
