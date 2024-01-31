namespace RealTimeChat.Models
{
    public class ChatViewModel
    {
        public IEnumerable<Chat> Chats { get; set; }
        public Chat SelectedChat { get; set; }
    }
}
