namespace RealTimeChat.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<string> ParticipantIds { get; set; }

        public List<Message> messages { get; set; }

    }
}
