using MonitorDataAccess.DataAccess;
using Domain.DTOs;
using System.Linq;
using MonitorDataAccess.Extensions;

namespace MoodModule.Services;

[AutoConstructor]
public partial class MoodDataAccessService
{
    //private readonly FileMoodDataAccess _fileDataAccess;
    //private readonly ImportFromMoodHistoryDataAccess _fileImportDataAccess;
    private readonly DBStatsDataAccess _dbStatsDataAccess;

    public async Task<List<StatsEntry>> GetAll()
    {
        return await _dbStatsDataAccess.GetAll();

        //var fileList = await _fileDataAccess.GetAll();
        //var importList = await _fileImportDataAccess.GetAll();

        //var mappedImportList = importList.Select(s => s.Map());

        //return Enumerable.Empty<StatsEntry>()
        //                 .Concat(fileList)
        //                 .Concat(mappedImportList)
        //                 .ToList();
    }

    public async Task<StatsEntry> Add(StatsEntry statsEntry)
    {
        return await _dbStatsDataAccess.Add(statsEntry);
    }

}
