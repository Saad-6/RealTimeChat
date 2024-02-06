namespace RealTimeChat.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public AppUser SenderUser { get; set; }
        public AppUser OtherUser { get; set; }
       

        public List<Message> messages { get; set; }

    public Chat()
    {

            messages = new List<Message>();
    }

    }
}
