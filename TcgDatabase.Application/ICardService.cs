using TcgDatabase.Application.DTOs;
using TcgDatabase.Domain;

namespace TcgDatabase.Application;

public interface ICardService
{
    Task<List<Card>> GetAllCards();
    Task<Card> AddCardAsync(AddCardRequest request);
}