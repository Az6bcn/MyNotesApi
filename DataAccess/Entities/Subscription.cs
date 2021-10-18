namespace DataAccess.Entities;

public record Subscription
{
    public int Id { get; set; }
    public string SubscriptionId { get; set; }
    public string Currency { get; set; }
    public DateTimeOffset TrialExpired { get; set; }
    public string Email { get; set; }

}