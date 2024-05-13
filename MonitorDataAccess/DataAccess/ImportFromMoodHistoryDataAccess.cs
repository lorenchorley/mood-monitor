using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.DataAccess;
using MonitorDataAccess.ExampleData;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MonitorDataAccess.DataAccess;

[AutoConstructor]
public partial class ImportFromMoodHistoryDataAccess : IDataAccess<MoodHistoryEntry>
{
    private readonly ITextDataSource[] _dataSources;

    public Task<MoodHistoryEntry> Add(MoodHistoryEntry entry)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MoodHistoryEntry>> GetAll()
    {
        List<MoodHistoryEntry> list = new();

        foreach (var dataSource in _dataSources)
        {
            string json = await dataSource.GetText();
            List<MoodHistoryEntry> rootObjects = JsonConvert.DeserializeObject<List<MoodHistoryEntry>>(json);
            list.AddRange(rootObjects);
        }

        return list;
    }
}
