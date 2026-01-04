namespace PrePurchase.Mobile.Shared.Models.Transaction;

public class CollectTransactionCommand
{
    public Guid TransactionId { get; set; }
    public string? CollectorName { get; set; }
    public Guid? AuthorizedCollectorId { get; set; }
    public List<CollectItemDto>? ItemsToCollect { get; set; }
}

public class CollectItemDto
{
    public Guid TransactionItemId { get; set; }
    public int QuantityCollected { get; set; }
}
