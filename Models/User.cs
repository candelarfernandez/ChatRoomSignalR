namespace ChatRoom.Models
{
    public class User
    {
        public string FullName { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }
}
