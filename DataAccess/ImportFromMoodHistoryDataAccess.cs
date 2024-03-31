namespace DataAccess;

[AutoConstructor]
public partial class ImportFromMoodHistoryDataAccess : IDataAccess
{
    private readonly string _filePath;

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
        throw new NotImplementedException();
    }
}
