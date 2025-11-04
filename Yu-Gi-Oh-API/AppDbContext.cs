using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Yu_Gi_Oh_Infrasturcture.Models;

namespace Yu_Gi_Oh_Infrasturcture;
/// <summary>
/// dotnet ef migrations add name
/// dotnet ef database update
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CardModel> Cards { get; set; }
    public DbSet<MonsterCardModel> MonsterCards { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardModel>()
            .HasOne(e => e.MonsterCard)
            .WithOne(e => e.Card)
            .HasForeignKey<MonsterCardModel>(e => e.CardId)
            .IsRequired(false);
    }
}