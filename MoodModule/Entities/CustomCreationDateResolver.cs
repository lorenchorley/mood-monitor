//using AutoMapper;
//using MonitorDataAccess.ExampleData;

//namespace MoodModule.Entities;

//public class CustomCreationDateResolver : IValueResolver<MoodHistoryEntry, MoodEntry, DateTime>
//{
//    public DateTime Resolve(MoodHistoryEntry source, MoodEntry destination, DateTime destMember, ResolutionContext context)
//    {
//        return DateTime.UnixEpoch.AddMilliseconds(source.date);
//    }
//}