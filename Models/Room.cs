namespace ChatRoom.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
        public Product product { get; set; }
        public ICollection<Bid> bids { get; set; }
    }
}
