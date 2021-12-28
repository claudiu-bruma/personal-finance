using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalFinance.Transactions.MessageProcessor.Configurations;
using PersonalFinance.Transactions.MessageProcessor.Entities; 

namespace PersonalFinance.Transactions.MessageProcessor.Services.CardTransactionServices
{
    public class CardTransactionService : ICardTransactionService
    {
        private readonly IMongoCollection<CardTransactionEntity> _cardTransactions;

        public CardTransactionService(
    IOptions<CardTransactionStoreDatabaseSettings> cardTransactionStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                cardTransactionStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                cardTransactionStoreDatabaseSettings.Value.DatabaseName);

            _cardTransactions = mongoDatabase.GetCollection<CardTransactionEntity>(
                cardTransactionStoreDatabaseSettings.Value.CardTransactionCollectionName);
        }

        public async Task<IReadOnlyList<CardTransactionEntity>> GetAsync() =>
            await _cardTransactions.Find(_ => true).ToListAsync();

        public async Task<CardTransactionEntity?> GetAsync(string id) =>
            await _cardTransactions.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CardTransactionEntity newCardTransaction) =>
            await _cardTransactions.InsertOneAsync(newCardTransaction);

        public async Task UpdateAsync(string id, CardTransactionEntity updatedCardTransaction) =>
            await _cardTransactions.ReplaceOneAsync(x => x.Id == id, updatedCardTransaction);

        public async Task RemoveAsync(string id) =>
            await _cardTransactions.DeleteOneAsync(x => x.Id == id);
    }
}
