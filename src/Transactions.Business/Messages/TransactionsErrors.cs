using ApiResults.CustomAttributes;
using System.ComponentModel;
using System.Net;

namespace Transactions.Business.Messages;

public enum TransactionsErrors
{
    [StatusCode(HttpStatusCode.BadRequest)]
    [Description("Requisição inválida.")]
    Application_Error_InvalidRequest,

    [StatusCode(HttpStatusCode.BadRequest)]
    [Description("Tipo da transação inválido.")]
    Transaction_Validation_InvalidType,

    [StatusCode(HttpStatusCode.BadRequest)]
    [Description("Categoria da transação inválida.")]
    Transaction_Validation_InvalidCategory,
}