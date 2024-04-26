using MonitorDataAccess.DataAccess;
using MonitorDataAccess.ExampleData;
using Newtonsoft.Json;

namespace MonitorDataAccess.DataAccess;

//[AutoConstructor]
public partial class ImportFromMoodHistoryDataAccess : IDataAccess<MoodHistoryEntry>
{
    private readonly string _filePath = @"C:\Users\lchorley\source\repos\mood-monitor\MonitorDataAccess\ExampleData\Moodistory 20240129 132739.json";

    public Task Add(MoodHistoryEntry entry)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MoodHistoryEntry>> GetAll()
    {
        using (StreamReader reader = new StreamReader(_filePath))
        {
            string json = await reader.ReadToEndAsync();
            List<MoodHistoryEntry> rootObjects = JsonConvert.DeserializeObject<List<MoodHistoryEntry>>(json);
            return rootObjects;
        }
    }
}
