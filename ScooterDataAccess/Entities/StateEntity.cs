namespace ScooterDataAccess.Entities;

public class StateEntity : BaseEntity
{
    public string? Availability { get; set; }
    public ICollection<ScooterEntity>? Scooters { get; set; }
}