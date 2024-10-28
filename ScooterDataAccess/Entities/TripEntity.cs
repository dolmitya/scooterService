namespace ScooterDataAccess.Entities;

public class TripEntity : BaseEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ScooterId { get; set; }
    public ScooterEntity Scooter { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public double PricePerMinute { get; set; }
}