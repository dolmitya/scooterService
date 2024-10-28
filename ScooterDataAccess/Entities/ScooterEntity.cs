namespace ScooterDataAccess.Entities;

public class ScooterEntity : BaseEntity
{
    public int Charge { get; set; }
    public string? PointLocation { get; set; }
    public int StateId { get; set; }
    public StateEntity State { get; set; }
    public int BreakId { get; set; }
    public BreakEntity Break { get; set; }
    public ICollection<TripEntity> Trips { get; set; }
    public ICollection<TechnicalInspectionEntity> TechnicalInspections { get; set; }
}