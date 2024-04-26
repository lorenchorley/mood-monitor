using Domain.DTOs;
using MonitorDataAccess.ExampleData;

namespace NotesModule.Entities;

public static class LogMappingProfile
{
    //public static LogEntry Map(this LogEntry entity)
    //{
    //    return new LogEntry
    //    {
    //        Creation = entity.Creation,
    //        Text = entity.Text
    //    };
    //}

    //public static LogEntry Map(this LogEntry dto)
    //{
    //    return new LogEntry
    //    {
    //        Creation = dto.Creation,
    //        Text = dto.Text
    //    };
    //}

    public static LogEntry Map(this LogHistoryEntry legacyEntry)
    {
        return new LogEntry
        {
            Creation = legacyEntry.Date,
            Annotation = legacyEntry.Annotation,
            Text = legacyEntry.NoteText
        };
    }
}

