using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using YuGiOh.Domain;

namespace YuGiOh.Infrastructure.Models;

[Table("Cards")]
public class CardModel
{
    [Key]
    public string Id{ get; set; } = null!;
    public string Name{ get; set; } = null!;
    public string Type{ get; set; } = null!;
    public string? HumanReadableCardType { get; set; }
    public string FrameType{ get; set; } = null!;
    public string Description{ get; set; } = null!;
    public string Race{ get; set; } = null!;
    public string? Archetype{ get; set; }
    public string? YgoprodeckUrl { get; set; }

    public MonsterCardModel? MonsterCard;
    public ICollection<CardSetModel> CardSets { get; set; } = new List<CardSetModel>();
    public ICollection<CardImageModel> CardImages { get; set; } = new List<CardImageModel>();
    public ICollection<CardPriceModel> CardPrices { get; set; } = new List<CardPriceModel>();

    public Card ToDtoModel()
    {
        return new Card
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description
        };
    }
}