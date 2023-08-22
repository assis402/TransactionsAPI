namespace Transactions.Shared.DTOs.Response;

public class DashboardResponseDTO
{
    public HighlightCard Income { get; set; }

    public HighlightCard Outcome { get; set; }

    public HighlightCard Sum { get; set; }

    public List<TransactionResponseDTO> Transactions { get; set; }
}

public class HighlightCard
{
    public HighlightCard(decimal total, string lastTransaction = null)
    {
        Total = total;
        LastTransaction = lastTransaction;
    }

    public decimal Total { get; set; }
    public string LastTransaction { get; set; }
}