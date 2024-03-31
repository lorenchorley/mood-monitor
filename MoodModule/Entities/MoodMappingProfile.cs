using Domain.DTOs;
using Domain.Types;
using MonitorDataAccess.ExampleData;

namespace MoodModule.Entities;

public static class MoodMappingProfile 
{
    //public static StatsEntry Map(this StatsEntry entity)
    //{
    //    return new StatsEntry
    //    {
    //        Creation = entity.Creation,
    //        DepressionRating = entity.DepressionRating,
    //        EnergyLevel = entity.EnergyLevel,
    //        Mood = entity.Mood
    //    };
    //}

    //public static StatsEntry Map(this StatsEntry dto)
    //{
    //    return new StatsEntry
    //    {
    //        Creation = dto.Creation,
    //        DepressionRating = dto.DepressionRating,
    //        EnergyLevel = dto.EnergyLevel,
    //        Mood = dto.Mood
    //    };
    //}

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

}

