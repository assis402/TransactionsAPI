using System.ComponentModel.DataAnnotations;

namespace TransactionsAPI.Entity;

public enum TransactionCategory 
{
    Purchases,
    Food,
    Salary,
    Automobile,
    Leisure,
    Studies,
    Others
}