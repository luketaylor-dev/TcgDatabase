using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YuGiOh.Infrastructure.Models;

[Table("CardSets")]
public class CardSetModel
{
    [Key]
    public int Id { get; set; }
    
    public string CardId { get; set; } = null!;
    public CardModel Card { get; set; } = null!;
    
    public string SetCode { get; set; } = null!;
    public string? SetName { get; set; }
    public string? SetRarity { get; set; }
    public string? SetRarityCode { get; set; }
    public string? SetPrice { get; set; }
}

