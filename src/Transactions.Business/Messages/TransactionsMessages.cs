using ApiResults.CustomAttributes;
using System.ComponentModel;
using System.Net;

namespace Transactions.Business.Messages
{
    public enum TransactionsMessages
    {
        [StatusCode(HttpStatusCode.Created)]
        [Description("Transação criada com sucesso.")]
        Transaction_Create_Success,
    }
}