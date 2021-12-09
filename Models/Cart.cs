namespace shhop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ThingId { get; set; }
        public Thing Thing { get; set; }
    }
}
