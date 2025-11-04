using Microsoft.Extensions.Logging;
using TcgDatabase.Application.DTOs;
using TcgDatabase.Domain;

namespace TcgDatabase.Application;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IYgoprodeckApiService _ygoprodeckApiService;
    private readonly ILogger<CardService> _logger;

    public CardService(
        ICardRepository cardRepository,
        IYgoprodeckApiService ygoprodeckApiService,
        ILogger<CardService> logger)
    {
        _cardRepository = cardRepository;
        _ygoprodeckApiService = ygoprodeckApiService;
        _logger = logger;
    }

    public async Task<List<Card>> GetAllCards()
    {
        return await _cardRepository.GetAllCards();
    }

    public async Task<Card> AddCardAsync(AddCardRequest request)
    {
        // Validate input - must provide either CardName or CardId
        if (string.IsNullOrWhiteSpace(request.CardName) && string.IsNullOrWhiteSpace(request.CardId))
        {
            throw new InvalidOperationException("Either CardName or CardId must be provided");
        }

        if (!string.IsNullOrWhiteSpace(request.CardName) && !string.IsNullOrWhiteSpace(request.CardId))
        {
            throw new InvalidOperationException("Provide either CardName OR CardId, not both");
        }

        // Fetch card data from YGOPRODeck API
        CardData? cardData;
        if (!string.IsNullOrWhiteSpace(request.CardId))
        {
            cardData = await _ygoprodeckApiService.GetCardByIdAsync(request.CardId);
            if (cardData == null)
            {
                throw new InvalidOperationException($"Card not found with ID: {request.CardId}");
            }
        }
        else
        {
            cardData = await _ygoprodeckApiService.GetCardByNameAsync(request.CardName!);
            if (cardData == null)
            {
                throw new InvalidOperationException($"Card not found: {request.CardName}");
            }
        }

        // Find the matching set in API response
        var matchingSet = cardData.CardSets?
            .FirstOrDefault(s => s.SetCode.Equals(request.SetId, StringComparison.OrdinalIgnoreCase));

        bool setFoundInApi = matchingSet != null;
        
        if (!setFoundInApi)
        {
            // Set not found in API response (likely a newer set not yet added to YGOPRODeck)
            var cardIdentifier = request.CardId ?? request.CardName ?? "Unknown";
            _logger.LogWarning(
                "Set '{SetId}' not found in API response for card '{CardIdentifier}'. Available sets: {AvailableSets}. Adding card with SetId but missing set details.",
                request.SetId,
                cardIdentifier,
                string.Join(", ", cardData.CardSets?.Select(s => s.SetCode) ?? Array.Empty<string>()));
        }

        // Check if card already exists
        var existingCard = await _cardRepository.GetCardByIdAsync(cardData.Id);
        if (existingCard != null)
        {
            _logger.LogInformation("Card {CardId} already exists, adding set: {SetId}", cardData.Id, request.SetId);
        }

        // Build CardCreateDto from API response
        var cardCreateDto = new CardCreateDto
        {
            Id = cardData.Id,
            Name = cardData.Name,
            Type = cardData.Type,
            HumanReadableCardType = cardData.HumanReadableCardType,
            FrameType = cardData.FrameType,
            Description = cardData.Description,
            Race = cardData.Race,
            Archetype = cardData.Archetype,
            YgoprodeckUrl = cardData.YgoprodeckUrl
        };

        // Add monster card data if available
        if (!string.IsNullOrEmpty(cardData.Attack) || !string.IsNullOrEmpty(cardData.Defence))
        {
            cardCreateDto.MonsterCard = new MonsterCardCreateDto
            {
                Attack = cardData.Attack ?? "0",
                Defence = cardData.Defence ?? "0",
                Level = cardData.Level,
                Attribute = cardData.Attribute,
                Scale = cardData.Scale,
                LinkValue = cardData.LinkValue,
                LinkMarkers = cardData.LinkMarkers
            };
        }

        // Add card images
        if (cardData.CardImages != null)
        {
            foreach (var imageData in cardData.CardImages)
            {
                cardCreateDto.CardImages.Add(new CardImageCreateDto
                {
                    ImageUrl = imageData.ImageUrl,
                    ImageUrlSmall = imageData.ImageUrlSmall,
                    ImageUrlCropped = imageData.ImageUrlCropped
                });
            }
        }

        // Add card prices
        if (cardData.CardPrices != null && cardData.CardPrices.Length > 0)
        {
            var priceData = cardData.CardPrices[0];
            cardCreateDto.CardPrices.Add(new CardPriceCreateDto
            {
                CardMarketPrice = priceData.CardMarketPrice,
                TcgPlayerPrice = priceData.TcgPlayerPrice,
                EbayPrice = priceData.EbayPrice,
                AmazonPrice = priceData.AmazonPrice,
                CoolStuffIncPrice = priceData.CoolStuffIncPrice
            });
        }

        // Add the set - either from API response or as placeholder if not found
        if (setFoundInApi && matchingSet != null)
        {
            // Set found in API - use full data
            cardCreateDto.CardSets.Add(new CardSetCreateDto
            {
                SetName = matchingSet.SetName,
                SetCode = matchingSet.SetCode,
                SetRarity = matchingSet.SetRarity,
                SetRarityCode = matchingSet.SetRarityCode,
                SetPrice = matchingSet.SetPrice
            });
        }
        else
        {
            // Set not found in API - add with SetId only, missing other details
            cardCreateDto.CardSets.Add(new CardSetCreateDto
            {
                SetCode = request.SetId,
                SetName = null,
                SetRarity = null,
                SetRarityCode = null,
                SetPrice = null
            });
        }

        // Save to database
        var savedCard = await _cardRepository.AddCardAsync(cardCreateDto);
        return savedCard;
    }
}