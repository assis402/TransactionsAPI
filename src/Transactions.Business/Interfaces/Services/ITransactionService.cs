using ApiResults;
using Transactions.Shared.DTOs.Request;

namespace Transactions.Business.Interfaces.Services;

public interface ITransactionService
{
    public Task<ApiResult> Create(TransactionCreateRequestDTO transactionDTO);
}