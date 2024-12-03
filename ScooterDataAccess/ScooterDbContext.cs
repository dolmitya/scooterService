using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<StateEntity> States { get; set; }
    public DbSet<TechnicalInspectionEntity> TechnicalInspections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
        modelBuilder.Entity<IdentityRole<int>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_roles_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
        
        modelBuilder.Entity<TechnicalInspectionEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ScooterEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<BreakEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<TripEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<StateEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);

        modelBuilder.Entity<ScooterEntity>().HasOne(x => x.State)
            .WithMany(x => x.Scooters).HasForeignKey(x => x.StateId);
        
        modelBuilder.Entity<ScooterEntity>().HasOne(x => x.Break)
            .WithMany(x => x.Scooters).HasForeignKey(x => x.BreakId);


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