//using Transactions.Business.Entities;
//using Transactions.Business.Enums;
//using Transactions.Shared.DTOs.Response;

//namespace Transactions.Business.Builders;

//public class DashboardBuilder
//{
//    private readonly DashboardResponseDTO _dashboard = new();

//    public DashboardResponseDTO Build() => _dashboard;

//    public DashboardBuilder SetTransactions(List<Transaction> transactions)
//    {
//        _dashboard.Transactions = transactions.Select(entity => (DashboardResponseDTO)entity)
//                                              .OrderByDescending(entity => entity.Date)
//                                              .ToList();
//        return this;
//    }

//    public DashboardBuilder SetIncome()
//    {
//        var incomeTransactions = _dashboard.Transactions.Where(dto => dto.Type == TransactionType.Income)
//                                                        .OrderByDescending(_ => _.Date);

//        var lastTransaction = incomeTransactions.FirstOrDefault().Date.ToString("dd de MMMM");
//        var totalIncome = incomeTransactions.Sum(_ => _.Amount);

//        _dashboard.Income = new HighlightCard(totalIncome, lastTransaction);

//        return this;
//    }

//    public DashboardBuilder SetOutcome()
//    {
//        var incomeTransactions = _dashboard.Transactions.Where(dto => dto.Type == TransactionType.Outcome)
//                                                        .OrderByDescending(_ => _.Date);

//        var lastTransaction = incomeTransactions.FirstOrDefault().Date.ToString("dd de MMMM");
//        var totalOutcome = incomeTransactions.Sum(_ => _.Amount);

//        _dashboard.Outcome = new HighlightCard(totalOutcome, lastTransaction);

//        return this;
//    }

//    public DashboardBuilder SetSum()
//    {
//        var total = _dashboard.Transactions.Sum(_ => _.Amount);
//        _dashboard.Sum = new HighlightCard(total);

//        return this;
//    }
//}