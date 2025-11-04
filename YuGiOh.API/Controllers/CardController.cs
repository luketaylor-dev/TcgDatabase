using YuGiOh.Application;
using YuGiOh.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace YuGiOh.API.Controllers
{
    /// <summary>
    /// Controller for managing Yu-Gi-Oh cards
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        /// <summary>
        /// Get all cards in the database
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IList<Domain.Card>>> Get()
        {
            var result = await _cardService.GetAllCards();
            return Ok(result);
        }

        /// <summary>
        /// Add a card to the database by fetching it from YGOPRODeck API
        /// </summary>
        /// <param name="request">
        /// Provide either CardName OR CardId (recommended), and SetId (required).
        /// If SetId is not found in API response (newer set), card will be added with SetId but missing set details.
        /// </param>
        /// <returns>The added card</returns>
        [HttpPost]
        public async Task<ActionResult<Domain.Card>> Post([FromBody] AddCardRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CardName) && string.IsNullOrWhiteSpace(request.CardId))
            {
                return BadRequest("Either CardName or CardId must be provided");
            }

            if (!string.IsNullOrWhiteSpace(request.CardName) && !string.IsNullOrWhiteSpace(request.CardId))
            {
                return BadRequest("Provide either CardName OR CardId, not both");
            }

            if (string.IsNullOrWhiteSpace(request.SetId))
            {
                return BadRequest("SetId is required");
            }

            try
            {
                var card = await _cardService.AddCardAsync(request);
                return CreatedAtAction(nameof(Get), new { id = card.Id }, card);
            }
            catch (InvalidOperationException ex)
            {
                var identifier = request.CardId ?? request.CardName ?? "Unknown";
                _logger.LogWarning(ex, "Failed to add card: {Identifier} with set: {SetId}", identifier, request.SetId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var identifier = request.CardId ?? request.CardName ?? "Unknown";
                _logger.LogError(ex, "Error adding card: {Identifier} with set: {SetId}", identifier, request.SetId);
                return StatusCode(500, "An error occurred while adding the card");
            }
        }
    }
}