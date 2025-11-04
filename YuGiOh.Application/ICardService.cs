using YuGiOh.Application.DTOs;
using YuGiOh.Domain;

namespace YuGiOh.Application;

public interface ICardService
{
    Task<List<Card>> GetAllCards();
    Task<Card> AddCardAsync(AddCardRequest request);
}