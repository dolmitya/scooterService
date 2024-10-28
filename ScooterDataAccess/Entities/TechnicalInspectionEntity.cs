namespace ScooterDataAccess.Entities;

public class TechnicalInspectionEntity : BaseEntity
{
    public int TechnicalSpecialistId { get; set; }
    public UserEntity TechnicalSpecialist { get; set; }
    public int ScooterId { get; set; }
    public ScooterEntity Scooter { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public string? JobDescription { get; set; }
}