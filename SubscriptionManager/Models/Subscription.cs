using SQLite;

namespace SubscriptionManager.Models
{
    public class Subscription
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public string Avatar { get; set; }

        [Ignore]
        public bool IsActive => DateTime.Today >= StartDate && DateTime.Today <= EndDate;
    }
}
