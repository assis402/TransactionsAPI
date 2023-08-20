using ApiResults.Helpers;
using FluentValidation;
using Transactions.Business.Enums;
using Transactions.Business.Messages;
using Transactions.Shared.DTOs.Request;
using static Transactions.Shared.Helpers.Utils;

namespace Transactions.Business.Validators;

public class TransactionCreateRequestDTOValidator : AbstractValidator<TransactionCreateRequestDTO>
{
    public TransactionCreateRequestDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Amount)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0.01M)
            .LessThan(10_000_000M);

        RuleFor(x => x.Type)
            .Must(ValidateEnum<TransactionType>)
            .WithMessage(TransactionsErrors.Transaction_Validation_InvalidType.Description());

        RuleFor(x => x.Category)
            .Must(ValidateEnum<TransactionCategory>)
            .WithMessage(TransactionsErrors.Transaction_Validation_InvalidCategory.Description());

        RuleFor(x => x.Date)
            .NotEmpty()
            .NotNull();
    }
}