using Microsoft.AspNetCore.Mvc;
using Transactions.Business.Interfaces.Services;
using Transactions.Shared.DTOs.Request;

namespace Transactions.API.Controllers;

[ApiController]
[Route("v1/[controller]/")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionCreateRequestDTO transactionDTO)
    {
        var result = await _transactionService.Create(transactionDTO);
        return result.Convert();
    }
}