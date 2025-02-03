namespace SubscriptionManager.Application.DTOs
{
    /// <summary>
    /// Obiekt transferu danych reprezentujący subskrypcję.
    /// </summary>
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Price { get; set; }
        public string AvatarPath { get; set; }
    }
}