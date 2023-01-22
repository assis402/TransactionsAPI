namespace Transactions.API.DTOs.Response;

public record struct DashboardResponseDTO
{
    public HighlightCard Income { get; set; }

    public HighlightCard Outcome { get; set; }

    public HighlightCard Sum { get; set; }

    public List<TransactionResponseDTO> Transactions { get; set; }
}

public readonly record struct HighlightCard(decimal Total, string? LastTransaction = null);