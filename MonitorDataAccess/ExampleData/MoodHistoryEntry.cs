using MonitorDataAccess.Extensions;

namespace MonitorDataAccess.ExampleData;

public class MoodHistoryEntry
{
    public A2locations a2locations { get; set; }
    public Answer2answerimage answer2answerimage { get; set; }
    public long date { get; set; }
    public string datetimezoneid { get; set; }
    public Event[] events { get; set; }
    public int id { get; set; }
    public int mood { get; set; }
    public string notes { get; set; }
}
