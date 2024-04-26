using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.DataAccess;
using MonitorDataAccess.ExampleData;
using Newtonsoft.Json;

namespace MonitorDataAccess.DataAccess;

public partial class ImportFromMoodHistoryDataAccess([FromKeyedServices("MoodHistoryData")] ITextDataSource dataSource) : IDataAccess<MoodHistoryEntry>
{

    public Task<MoodHistoryEntry> Add(MoodHistoryEntry entry)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MoodHistoryEntry>> GetAll()
    {
        string json = await dataSource.GetText();
        List<MoodHistoryEntry> rootObjects = JsonConvert.DeserializeObject<List<MoodHistoryEntry>>(json);
        return rootObjects;
    }
}
