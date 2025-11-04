using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TcgDatabase.Infrastructure.Models;

[Table("CardPrices")]
public class CardPriceModel
{
    [Key]
    public int Id { get; set; }
    
    public string CardId { get; set; } = null!;
    public CardModel Card { get; set; } = null!;
    
    public string? CardMarketPrice { get; set; }
    public string? TcgPlayerPrice { get; set; }
    public string? EbayPrice { get; set; }
    public string? AmazonPrice { get; set; }
    public string? CoolStuffIncPrice { get; set; }
}

