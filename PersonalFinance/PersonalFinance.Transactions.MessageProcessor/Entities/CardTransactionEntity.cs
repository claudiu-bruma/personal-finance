using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalFinance.Transactions.MessageProcessor.Entities
{
    public class CardTransactionEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public decimal Value { get; set; }
        public string? SellerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public string? TransactionReference { get; set; }
        public string? TransactionState { get; set; }
    }
}
