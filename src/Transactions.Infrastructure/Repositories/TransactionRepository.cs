using Transactions.Business.Entities;
using Transactions.Business.Interfaces.Repositories;

namespace Transactions.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TransactionsContextDb context) : base(context)
    {
    }
}