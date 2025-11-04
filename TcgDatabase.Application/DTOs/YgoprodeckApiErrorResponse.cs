using System.Text.Json.Serialization;

namespace TcgDatabase.Application.DTOs;

public class YgoprodeckApiErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = null!;
}


