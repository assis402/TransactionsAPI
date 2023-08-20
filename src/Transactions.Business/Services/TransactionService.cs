using ApiResults;
using Transactions.Business.Entities;
using Transactions.Business.Interfaces.Repositories;
using Transactions.Business.Interfaces.Services;
using Transactions.Business.Messages;
using Transactions.Business.Validators;
using Transactions.Shared.DTOs.Request;
using Transactions.Shared.DTOs.Response;

namespace Transactions.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ApiResult> Create(TransactionCreateRequestDTO transactionDTO)
        {
            var validation = new TransactionCreateRequestDTOValidator().Validate(transactionDTO);

            if (!validation.IsValid)
                return Result.Error(
                    TransactionsErrors.Application_Error_InvalidRequest,
                    validation.Errors);

            var transaction = new Transaction(transactionDTO);
            await _transactionRepository.InsertOneAsync(transaction);

            return Result.Success(TransactionsMessages.Transaction_Create_Success, (TransactionResponseDTO)transaction);
        }
    }
}