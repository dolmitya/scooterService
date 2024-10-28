using Microsoft.EntityFrameworkCore;
using ScooterDataAccess.Entities;
 
namespace ScooterDataAccess;

public class ScooterDbContext : DbContext
{
    public ScooterDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ScooterEntity> Scooters { get; set; }
    public DbSet<BreakEntity> Breaks { get; set; }
    public DbSet<TripEntity> Trips { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<StateEntity> States { get; set; }
    public DbSet<TechnicalInspectionEntity> TechnicalInspections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TechnicalInspectionEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ScooterEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<BreakEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TripEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<RoleEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<StateEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);

        modelBuilder.Entity<ScooterEntity>().HasOne(x => x.State)
            .WithMany(x => x.Scooters).HasForeignKey(x => x.StateId);
        
        modelBuilder.Entity<ScooterEntity>().HasOne(x => x.Break)
            .WithMany(x => x.Scooters).HasForeignKey(x => x.BreakId);

        modelBuilder.Entity<UserEntity>().HasOne(x => x.Role)
            .WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

        modelBuilder.Entity<TripEntity>().HasOne(x => x.User)
            .WithMany(x => x.Trips).HasForeignKey(x => x.UserId);

        modelBuilder.Entity<TripEntity>().HasOne(x => x.Scooter)
            .WithMany(x => x.Trips).HasForeignKey(x => x.ScooterId);
        

        modelBuilder.Entity<TechnicalInspectionEntity>().HasOne(x => x.Scooter)
            .WithMany(x => x.TechnicalInspections).HasForeignKey(x => x.ScooterId);

        modelBuilder.Entity<TechnicalInspectionEntity>().HasOne(x => x.TechnicalSpecialist)
            .WithMany(x => x.TechnicalInspections).HasForeignKey(x => x.TechnicalSpecialistId);
    }
}