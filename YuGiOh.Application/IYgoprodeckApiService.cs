using YuGiOh.Application.DTOs;

namespace YuGiOh.Application;

public interface IYgoprodeckApiService
{
    Task<CardData?> GetCardByNameAsync(string cardName);
    Task<CardData?> GetCardByIdAsync(string cardId);
}

