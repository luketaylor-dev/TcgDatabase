using YuGiOh.Application.DTOs;
using YuGiOh.Domain;

namespace YuGiOh.Application;

public interface ICardRepository
{
    Task<List<Card>> GetAllCards();
    Task<Card?> GetCardByIdAsync(string cardId);
    Task<Card> AddCardAsync(CardCreateDto cardDto);
    Task<bool> CardExistsAsync(string cardId);
}