using Domain.Types;

namespace Domain.DTOs;

public class StatsEntry
{
    public int Id { get; set; }
    public DateTime Creation { get; set; }
    public MoodValue? Mood { get; set; }
    public EnergyValue? EnergyLevel { get; set; }
    public DepressionValue? DepressionRating { get; set; }
}