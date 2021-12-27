using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Api.Models.Transactions;
using PersonalFinance.Api.Services.MessagingServices;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardTransactionsController : ControllerBase
    {
        private IMessagingService _messagingService;

        public CardTransactionsController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpGet("interval/{intervalStart}/{intervalEnd}")]
        public IReadOnlyCollection<CardTransation> GetByInterval(DateTime intervalStart, DateTime intervalEnd)
        {
            return new List<CardTransation>();
        }
        [HttpGet("reference/{reference}")]
        public CardTransation GetByReference(string reference)
        {
            return new CardTransation();
        }
        [HttpPost()]
        public async Task CreateTransaction(CardTransation cardTransation)
        {
            await _messagingService.PublishNewCardTransaction(cardTransation);
        }
        [HttpPatch("authorization/{reference}")]
        public void AuthorizeTransaction(string reference)
        {

        }
        [HttpDelete()]
        public void RollBackTransaction(string reference)
        {

        }
    }
}
