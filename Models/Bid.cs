namespace ChatRoom.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public User FromUser { get; set; }
        public int ToRoomId { get; set; }
        public Room Room { get; set; }
    }
}
