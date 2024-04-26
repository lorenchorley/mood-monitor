using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DTOs;

public class LogEntry
{
    public int Id { get; set; }
    public DateOnly Creation { get; set; }
    public string Annotation { get; set; } = string.Empty;

    [NotMapped]
    [JsonIgnore]
    public string Text { get; set; }

    public string EncryptedText { get; set; }

}
