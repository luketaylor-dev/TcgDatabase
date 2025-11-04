using TcgDatabase.Application.DTOs;

namespace TcgDatabase.Application;

public interface IYgoprodeckApiService
{
    Task<CardData?> GetCardByNameAsync(string cardName);
    Task<CardData?> GetCardByIdAsync(string cardId);
}

