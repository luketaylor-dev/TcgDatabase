using System.Text.Json.Serialization;

namespace YuGiOh.Application.DTOs;

public class YgoprodeckApiErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = null!;
}


