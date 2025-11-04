using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TcgDatabase.Infrastructure.Models;

[Table("CardImages")]
public class CardImageModel
{
    [Key]
    public int Id { get; set; }
    
    public string CardId { get; set; } = null!;
    public CardModel Card { get; set; } = null!;
    
    public string ImageUrl { get; set; } = null!;
    public string ImageUrlSmall { get; set; } = null!;
    public string ImageUrlCropped { get; set; } = null!;
}

