using Newtonsoft.Json;

namespace Domain.DTOs;

public class LogEntry
{
    public int Id { get; set; }
    public DateOnly Creation { get; set; }

    [JsonIgnore]
    public string Text { get; set; }

    public string EncryptedText { get; set; }

}
