using System.Text.Json;

namespace Models
{
    public record WebHookSubscriptionCreatedDto
    {
        public string? SubscriptionId { get; init; }
        public string? UserId { get; init; }
        public string? EventType { get; init; }
        public string? PlanName { get; init; }
        public double Price { get; init; }
        public string? Currency { get; init; }
        public string? CustomerId { get; init; }
        public int TrialDays { get; init; }
        public DateTimeOffset TrialExpiryDate { get; init; }
        public string? Email { get; init; }


        public static WebHookSubscriptionCreatedDto TryGetPropertyValues(JsonElement message)
        {
            var data = message.GetProperty("data");
            var plan = data.GetProperty("plan");

            return new()
            {
                SubscriptionId = data.GetProperty("id").GetString(),
                UserId = plan.GetProperty("user_id").GetString(),
                EventType = message.GetProperty("event_type").ToString(),
                PlanName = plan.GetProperty("plan_name").GetString(),
                Price = plan.GetProperty("price").GetDouble(),
                Currency = data.GetProperty("currency_symbol").GetString(),
                CustomerId = data.GetProperty("customer_id").GetString(),
                TrialDays = data.GetProperty("trial_days").GetInt32(),
                TrialExpiryDate = data.GetProperty("trial_expiry_date").GetDateTimeOffset(),
                Email = data.GetProperty("email_id").GetString()
            };
        }
    }
}