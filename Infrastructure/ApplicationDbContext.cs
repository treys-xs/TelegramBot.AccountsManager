using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain;
using Application.Interfaces;
using Infrastructure.Configurations;

namespace Infrastructure;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options, 
    IConfiguration configuration)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString("Database"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}