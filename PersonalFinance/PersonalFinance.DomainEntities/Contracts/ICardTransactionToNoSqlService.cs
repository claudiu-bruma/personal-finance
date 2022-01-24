using PersonalFinance.Transactions.MessageProcessor.Entities;

namespace PersonalFinance.Domain.Contracts
{
    public interface ICardTransactionToNoSqlService
    {
        Task CreateAsync(CardTransactionMongoDbEntity newCardTransaction);
        Task<IReadOnlyList<CardTransactionMongoDbEntity>> GetAsync();
        Task<CardTransactionMongoDbEntity?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, CardTransactionMongoDbEntity updatedCardTransaction);
    }
}