using System.Text.Json.Serialization;

namespace YuGiOh.Application.DTOs;

public class YgoprodeckApiResponse
{
    [JsonPropertyName("data")]
    public CardData[]? Data { get; set; }
}

public class CardData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("humanReadableCardType")]
    public string? HumanReadableCardType { get; set; }

    [JsonPropertyName("frameType")]
    public string FrameType { get; set; } = null!;

    [JsonPropertyName("desc")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("race")]
    public string Race { get; set; } = null!;

    [JsonPropertyName("archetype")]
    public string? Archetype { get; set; }

    [JsonPropertyName("ygoprodeck_url")]
    public string? YgoprodeckUrl { get; set; }

    [JsonPropertyName("atk")]
    public string? Attack { get; set; }

    [JsonPropertyName("def")]
    public string? Defence { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("attribute")]
    public string? Attribute { get; set; }

    [JsonPropertyName("scale")]
    public string? Scale { get; set; }

    [JsonPropertyName("linkval")]
    public string? LinkValue { get; set; }

    [JsonPropertyName("linkmarkers")]
    public string[]? LinkMarkers { get; set; }

    [JsonPropertyName("card_sets")]
    public CardSetData[]? CardSets { get; set; }

    [JsonPropertyName("card_images")]
    public CardImageData[]? CardImages { get; set; }

    [JsonPropertyName("card_prices")]
    public CardPriceData[]? CardPrices { get; set; }
}

public class CardSetData
{
    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = null!;

    [JsonPropertyName("set_code")]
    public string SetCode { get; set; } = null!;

    [JsonPropertyName("set_rarity")]
    public string SetRarity { get; set; } = null!;

    [JsonPropertyName("set_rarity_code")]
    public string SetRarityCode { get; set; } = null!;

    [JsonPropertyName("set_price")]
    public string SetPrice { get; set; } = null!;
}

public class CardImageData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = null!;

    [JsonPropertyName("image_url_small")]
    public string ImageUrlSmall { get; set; } = null!;

    [JsonPropertyName("image_url_cropped")]
    public string ImageUrlCropped { get; set; } = null!;
}

public class CardPriceData
{
    [JsonPropertyName("cardmarket_price")]
    public string? CardMarketPrice { get; set; }

    [JsonPropertyName("tcgplayer_price")]
    public string? TcgPlayerPrice { get; set; }

    [JsonPropertyName("ebay_price")]
    public string? EbayPrice { get; set; }

    [JsonPropertyName("amazon_price")]
    public string? AmazonPrice { get; set; }

    [JsonPropertyName("coolstuffinc_price")]
    public string? CoolStuffIncPrice { get; set; }
}

