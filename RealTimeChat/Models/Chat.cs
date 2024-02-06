namespace RealTimeChat.Models
{
    public class Chat
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public AppUser SenderUser { get; set; }
        public AppUser OtherUser { get; set; }
       

        public List<Message> messages { get; set; }

    public Chat()
    {

            messages = new List<Message>();
    }
=======
        public List<string> ParticipantIds { get; set; }

        public List<Message> messages { get; set; }

>>>>>>> 122d20511a43c7b31552552180044f743114b4bb
    }
}
