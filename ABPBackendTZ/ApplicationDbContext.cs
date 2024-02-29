using ABPBackendTZ.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<ButtonColor> ButtonColors { get; set; }
    public DbSet<PriceToShow> PricesToShow { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ButtonColor>().HasData(new ButtonColor
        {
            Id = 1,
            HEX = "#FF0000",
        }, new ButtonColor
        {
            Id = 2,
            HEX = "#00FF00",
        }, new ButtonColor
        {
            Id = 3,
            HEX = "#0000FF",
        });

        builder.Entity<PriceToShow>().HasData(new
        {
            Id = 1,
            Value = 10M,
            Percentage = 0.75F,
        }, new
        {
            Id = 2,
            Value = 20M,
            Percentage = 0.1F,
        }, new
        {
            Id = 3,
            Value = 50M,
            Percentage = 0.05F,
        }, new
        {
            Id = 4,
            Value = 5M,
            Percentage = 0.1F,
        });
    }
}