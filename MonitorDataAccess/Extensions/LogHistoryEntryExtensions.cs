using Domain.DTOs;
using MonitorDataAccess.ExampleData;

namespace MonitorDataAccess.Extensions;

public static class LogHistoryEntryExtensions
{
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

