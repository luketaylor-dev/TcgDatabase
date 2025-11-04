using TcgDatabase.Application.DTOs;
using TcgDatabase.Domain;

namespace TcgDatabase.Application;

public interface ICardRepository
{
    Task<List<Card>> GetAllCards();
    Task<Card?> GetCardByIdAsync(string cardId);
    Task<Card> AddCardAsync(CardCreateDto cardDto);
    Task<bool> CardExistsAsync(string cardId);
}