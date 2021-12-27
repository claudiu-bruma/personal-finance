namespace PersonalFinance.Api.Models.Transactions
{
    public class CardTransation
    {
        public decimal Value { get; set; }
        public string? SellerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public string? TransactionReference { get; set; }
        public string? TransactionState { get; set; }

    }
}
