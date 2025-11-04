using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yu_Gi_Oh_Infrasturcture.Models;

[Table("Cards")]
public class CardModel
{
    [Key]
    public string Id{ get; set; }
    public string Name{ get; set; }
    public string Type{ get; set; }
    public string FrameType{ get; set; }
    public string Description{ get; set; }
    public string Race{ get; set; }
    public string? Archetype{ get; set; }

    public MonsterCardModel? MonsterCard;
}