namespace ScooterDataAccess.Entities;

public class BreakEntity : BaseEntity
{
    public string? NameBreakdown { get; set; }
    public ICollection<ScooterEntity>? Scooters { get; set; }
}