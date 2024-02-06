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
