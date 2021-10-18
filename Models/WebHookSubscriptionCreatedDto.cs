using System.Text.Json;

namespace Models
{
    public class WebHookSubscriptionCreatedDto
    {
        public string SubscriptionId { get; set; }
        public string UserId { get; set; }
        public string EventType { get; set; }
        public string PlanName { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
        public int TrialDays { get; set; }
        public DateTimeOffset TrialExpiryDate { get; set; }
        public string Email { get; set; }


        public void TryGetPropertyValues(JsonElement message)
        {
            var data = message.GetProperty("data");
            var plan = data.GetProperty("plan");

            SubscriptionId = data.GetProperty("id").GetString();
            UserId = plan.GetProperty("user_id").GetString();
            EventType = message.GetProperty("event_type").ToString();
            PlanName = plan.GetProperty("plan_name").GetString();
            Price = plan.GetProperty("price").GetDouble();
            Currency = data.GetProperty("currency_symbol").GetString();
            CustomerId = data.GetProperty("customer_id").GetString();
            TrialDays = data.GetProperty("trial_days").GetInt32();
            TrialExpiryDate = data.GetProperty("trial_expiry_date").GetDateTimeOffset();
            Email = data.GetProperty("email_id").GetString();

        }
    }
}