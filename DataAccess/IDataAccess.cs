namespace DataAccess;

public interface IDataAccess
{
    Task<List<MoodEntry>> GetAllMoodEntries();
    Task<List<LogEntry>> GetAllLogEntries();
    Task<List<EnergyEntry>> GetAllEnergyEntries();
}
