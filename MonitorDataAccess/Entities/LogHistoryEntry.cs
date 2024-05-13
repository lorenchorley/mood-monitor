using MonitorDataAccess.Extensions;

namespace MonitorDataAccess.ExampleData;

public class LogHistoryEntry
{
    public DateOnly Date { get; set; }
    public string Annotation { get; set; } = string.Empty;
    public string NoteText { get; set; }
}
