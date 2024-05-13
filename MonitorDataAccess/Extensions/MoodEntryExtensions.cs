using Domain.DTOs;
using Domain.Types;
using MonitorDataAccess.ExampleData;
using Newtonsoft.Json;

namespace MonitorDataAccess.Extensions;

public static class MoodEntryExtensions
{
    public static StatsEntry Map(this MoodHistoryEntry legacyEntry)
    {
        return new StatsEntry
        {
            Creation = DateTime.UnixEpoch.AddMilliseconds(legacyEntry.date),
            //DepressionRating = legacyEntry.DepressionRating,
            //EnergyLevel = legacyEntry.EnergyLevel,
            Mood = (MoodValue)legacyEntry.mood
        };
    }

    public static string SerialiseToList(this List<StatsEntry> list)
    {
        return JsonConvert.SerializeObject(list);
    }

    public static List<StatsEntry> DeserialiseToList(this string json)
    {
        return JsonConvert.DeserializeObject<List<StatsEntry>>(json);
    }

}