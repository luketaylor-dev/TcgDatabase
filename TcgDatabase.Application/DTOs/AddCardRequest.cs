namespace TcgDatabase.Application.DTOs;

public class AddCardRequest
{
    /// <summary>
    /// Card name (exact match) - use this OR CardId, not both
    /// </summary>
    public string? CardName { get; set; }
    
    /// <summary>
    /// Card ID (8-digit passcode) - use this OR CardName, not both. More reliable than name.
    /// </summary>
    public string? CardId { get; set; }
    
    /// <summary>
    /// Set ID (e.g., "JUSH-EN040"). Required. If the set is not in the API response (newer set), 
    /// the card will still be added with this SetId but missing set details.
    /// </summary>
    public string SetId { get; set; } = null!;
}

