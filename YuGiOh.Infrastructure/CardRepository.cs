using Microsoft.EntityFrameworkCore;
using YuGiOh.Application;
using YuGiOh.Application.DTOs;
using YuGiOh.Domain;
using YuGiOh.Infrastructure.Models;

namespace YuGiOh.Infrastructure;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllCards()
    {
        var cardModels = await _context.Cards
            .Include(c => c.CardSets)
            .Include(c => c.CardImages)
            .Include(c => c.CardPrices)
            .Include(c => c.MonsterCard)
            .ToListAsync();
        return cardModels.Select(x => x.ToDtoModel()).ToList();
    }

    public async Task<Card?> GetCardByIdAsync(string cardId)
    {
        var cardModel = await _context.Cards
            .Include(c => c.CardSets)
            .Include(c => c.CardImages)
            .Include(c => c.CardPrices)
            .Include(c => c.MonsterCard)
            .FirstOrDefaultAsync(c => c.Id == cardId);
        return cardModel?.ToDtoModel();
    }

    public async Task<Card> AddCardAsync(CardCreateDto cardDto)
    {
        var existingCard = await _context.Cards
            .Include(c => c.CardSets)
            .Include(c => c.CardImages)
            .Include(c => c.CardPrices)
            .Include(c => c.MonsterCard)
            .FirstOrDefaultAsync(c => c.Id == cardDto.Id);

        CardModel cardModel;
        
        if (existingCard == null)
        {
            // Create new card
            cardModel = new CardModel
            {
                Id = cardDto.Id,
                Name = cardDto.Name,
                Type = cardDto.Type,
                HumanReadableCardType = cardDto.HumanReadableCardType,
                FrameType = cardDto.FrameType,
                Description = cardDto.Description,
                Race = cardDto.Race,
                Archetype = cardDto.Archetype,
                YgoprodeckUrl = cardDto.YgoprodeckUrl
            };

            // Add monster card
            if (cardDto.MonsterCard != null)
            {
                cardModel.MonsterCard = new MonsterCardModel
                {
                    CardId = cardDto.Id,
                    Attack = cardDto.MonsterCard.Attack,
                    Defence = cardDto.MonsterCard.Defence,
                    Level = cardDto.MonsterCard.Level,
                    Attribute = cardDto.MonsterCard.Attribute,
                    Scale = cardDto.MonsterCard.Scale,
                    LinkValue = cardDto.MonsterCard.LinkValue,
                    LinkMarkers = cardDto.MonsterCard.LinkMarkers
                };
            }

            // Add images
            foreach (var imageDto in cardDto.CardImages)
            {
                cardModel.CardImages.Add(new CardImageModel
                {
                    CardId = cardDto.Id,
                    ImageUrl = imageDto.ImageUrl,
                    ImageUrlSmall = imageDto.ImageUrlSmall,
                    ImageUrlCropped = imageDto.ImageUrlCropped
                });
            }

            // Add prices
            foreach (var priceDto in cardDto.CardPrices)
            {
                cardModel.CardPrices.Add(new CardPriceModel
                {
                    CardId = cardDto.Id,
                    CardMarketPrice = priceDto.CardMarketPrice,
                    TcgPlayerPrice = priceDto.TcgPlayerPrice,
                    EbayPrice = priceDto.EbayPrice,
                    AmazonPrice = priceDto.AmazonPrice,
                    CoolStuffIncPrice = priceDto.CoolStuffIncPrice
                });
            }

            // Add sets
            foreach (var setDto in cardDto.CardSets)
            {
                cardModel.CardSets.Add(new CardSetModel
                {
                    CardId = cardDto.Id,
                    SetName = setDto.SetName,
                    SetCode = setDto.SetCode,
                    SetRarity = setDto.SetRarity,
                    SetRarityCode = setDto.SetRarityCode,
                    SetPrice = setDto.SetPrice
                });
            }

            _context.Cards.Add(cardModel);
        }
        else
        {
            // Card exists, add new sets that don't exist
            cardModel = existingCard;
            
            foreach (var setDto in cardDto.CardSets)
            {
                if (!cardModel.CardSets.Any(s => s.SetCode.Equals(setDto.SetCode, StringComparison.OrdinalIgnoreCase)))
                {
                    cardModel.CardSets.Add(new CardSetModel
                    {
                        CardId = cardDto.Id,
                        SetName = setDto.SetName,
                        SetCode = setDto.SetCode,
                        SetRarity = setDto.SetRarity,
                        SetRarityCode = setDto.SetRarityCode,
                        SetPrice = setDto.SetPrice
                    });
                }
            }
        }

        await _context.SaveChangesAsync();
        return cardModel.ToDtoModel();
    }

    public async Task<bool> CardExistsAsync(string cardId)
    {
        return await _context.Cards.AnyAsync(c => c.Id == cardId);
    }
}