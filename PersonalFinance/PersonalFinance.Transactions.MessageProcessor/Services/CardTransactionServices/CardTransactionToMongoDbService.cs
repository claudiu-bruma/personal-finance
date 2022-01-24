using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalFinance.Transactions.MessageProcessor.Configurations;
using PersonalFinance.Transactions.MessageProcessor.Entities;
using PersonalFinance.Domain.Contracts;

namespace PersonalFinance.Transactions.MessageProcessor.Services.CardTransactionServices
{
    public class CardTransactionToMongoDbService : ICardTransactionToNoSqlService
    {
        private readonly IMongoCollection<CardTransactionMongoDbEntity> _cardTransactions;

        public CardTransactionToMongoDbService(
    IOptions<CardTransactionStoreDatabaseSettings> cardTransactionStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                cardTransactionStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                cardTransactionStoreDatabaseSettings.Value.DatabaseName);

            _cardTransactions = mongoDatabase.GetCollection<CardTransactionMongoDbEntity>(
                cardTransactionStoreDatabaseSettings.Value.CardTransactionCollectionName);
        }

        public async Task<IReadOnlyList<CardTransactionMongoDbEntity>> GetAsync() =>
            await _cardTransactions.Find(_ => true).ToListAsync();

        public async Task<CardTransactionMongoDbEntity?> GetAsync(string id) =>
            await _cardTransactions.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CardTransactionMongoDbEntity newCardTransaction) =>
            await _cardTransactions.InsertOneAsync(newCardTransaction);

        public async Task UpdateAsync(string id, CardTransactionMongoDbEntity updatedCardTransaction) =>
            await _cardTransactions.ReplaceOneAsync(x => x.Id == id, updatedCardTransaction);

        public async Task RemoveAsync(string id) =>
            await _cardTransactions.DeleteOneAsync(x => x.Id == id);
    }
}
