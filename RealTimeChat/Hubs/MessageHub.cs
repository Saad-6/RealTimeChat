using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using RealTimeChat.Data;
using RealTimeChat.Models;
using System.Security.Claims;
using Message = RealTimeChat.Models.Message;

namespace RealTimeChat.Hubs
{
    public class MessageHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MessageHub(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task SendMessageHub(string query,string recieverId)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          //  var recieverId = recieverId;
            var senderUser = _context.Users.FirstOrDefault(m => m.Id==userId);
            var recieverUser = _context.Users.FirstOrDefault(m => m.Id == recieverId);
            var newMessage = new Message();
            newMessage.message = query;
            newMessage.Sender = senderUser;
            newMessage.Reciever = recieverUser;
            _context.Messages.Add(newMessage);

            var chat=_context.Chats.Where(m=>m.SenderUser.Id==userId && m.OtherUser.Id==recieverId).FirstOrDefault();
            if (chat == null)
            {
             chat=_context.Chats.Where(m=>m.SenderUser.Id==recieverId && m.OtherUser.Id==userId).FirstOrDefault();

            }
            chat.messages.Add(newMessage);
            await _context.SaveChangesAsync();

            var latestMessage = chat.messages.Last().message;
            string IsSenderUser = "yes";
            string notSender = null;
            
            await Clients.User(userId).SendAsync("RecieveMessage", latestMessage,IsSenderUser);
            await Clients.User(recieverId).SendAsync("RecieveMessage", latestMessage,notSender);

        }
    }
}