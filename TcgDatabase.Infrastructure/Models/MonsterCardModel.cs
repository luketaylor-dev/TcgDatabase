using System.ComponentModel.DataAnnotations;

namespace TcgDatabase.Infrastructure.Models;

public class MonsterCardModel
{
    [Key]
    public int Id { get; set; }
    public string Attack{ get; set; }
    public string Defence{ get; set; }
    public string Level{ get; set; }
    public string Attribute{ get; set; }
    public string? Scale{ get; set; }
    public string? LinkValue{ get; set; }
    public string[]? LinkMarkers{ get; set; }

    public string? CardId { get; set; }
    public CardModel? Card { get; set; } = null!;
}