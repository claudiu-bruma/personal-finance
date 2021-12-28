using PersonalFinance.Transactions.MessageProcessor.Entities;

namespace PersonalFinance.Transactions.MessageProcessor.Services.CardTransactionServices
{
    public interface ICardTransactionService
    {
        Task CreateAsync(CardTransactionEntity newCardTransaction);
        Task<IReadOnlyList<CardTransactionEntity>> GetAsync();
        Task<CardTransactionEntity?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, CardTransactionEntity updatedCardTransaction);
    }
}