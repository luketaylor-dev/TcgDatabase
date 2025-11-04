using Microsoft.EntityFrameworkCore;
using YuGiOh.Infrastructure.Models;

namespace YuGiOh.Infrastructure;
 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CardModel> Cards { get; set; }
    public DbSet<MonsterCardModel> MonsterCards { get; set; }
    public DbSet<CardSetModel> CardSets { get; set; }
    public DbSet<CardImageModel> CardImages { get; set; }
    public DbSet<CardPriceModel> CardPrices { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardModel>()
            .HasOne(e => e.MonsterCard)
            .WithOne(e => e.Card)
            .HasForeignKey<MonsterCardModel>(e => e.CardId)
            .IsRequired(false);

        modelBuilder.Entity<CardSetModel>()
            .HasOne(e => e.Card)
            .WithMany(e => e.CardSets)
            .HasForeignKey(e => e.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardImageModel>()
            .HasOne(e => e.Card)
            .WithMany(e => e.CardImages)
            .HasForeignKey(e => e.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardPriceModel>()
            .HasOne(e => e.Card)
            .WithMany(e => e.CardPrices)
            .HasForeignKey(e => e.CardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}