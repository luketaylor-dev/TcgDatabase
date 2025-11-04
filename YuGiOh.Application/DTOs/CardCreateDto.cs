namespace YuGiOh.Application.DTOs;

public class CardCreateDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? HumanReadableCardType { get; set; }
    public string FrameType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Race { get; set; } = null!;
    public string? Archetype { get; set; }
    public string? YgoprodeckUrl { get; set; }
    
    public MonsterCardCreateDto? MonsterCard { get; set; }
    public List<CardSetCreateDto> CardSets { get; set; } = new();
    public List<CardImageCreateDto> CardImages { get; set; } = new();
    public List<CardPriceCreateDto> CardPrices { get; set; } = new();
}

public class MonsterCardCreateDto
{
    public string Attack { get; set; } = null!;
    public string Defence { get; set; } = null!;
    public string? Level { get; set; }
    public string? Attribute { get; set; }
    public string? Scale { get; set; }
    public string? LinkValue { get; set; }
    public string[]? LinkMarkers { get; set; }
}

public class CardSetCreateDto
{
    public string SetCode { get; set; } = null!;
    public string? SetName { get; set; }
    public string? SetRarity { get; set; }
    public string? SetRarityCode { get; set; }
    public string? SetPrice { get; set; }
}

public class CardImageCreateDto
{
    public string ImageUrl { get; set; } = null!;
    public string ImageUrlSmall { get; set; } = null!;
    public string ImageUrlCropped { get; set; } = null!;
}

public class CardPriceCreateDto
{
    public string? CardMarketPrice { get; set; }
    public string? TcgPlayerPrice { get; set; }
    public string? EbayPrice { get; set; }
    public string? AmazonPrice { get; set; }
    public string? CoolStuffIncPrice { get; set; }
}

