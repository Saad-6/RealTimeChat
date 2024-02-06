using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Data;
using RealTimeChat.Models;

namespace RealTimeChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ChatController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public IActionResult MyChats()
        {
            if (!User.Identity.IsAuthenticated)
            {
              return  RedirectToAction("Index", "Home");
            }
            var ThisUser = _userManager.GetUserAsync(User).Result;
            var ThisUserId = ThisUser.Id;
           var ThisUserChats = _context.Chats.Include(m=>m.messages).ThenInclude(m=>m.Reciever).Include(m=>m.messages).ThenInclude(m=>m.Sender).Include(m=>m.OtherUser).Include(m=>m.SenderUser).Where(m => m.SenderUser.Id==ThisUserId ||m.OtherUser.Id==ThisUserId).ToList();
            if (ThisUserChats == null || ThisUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("chat",ThisUserChats);
        }
       
        public IActionResult Index(string id)
        {
            var user=_context.Users.FirstOrDefault(x => x.Id == id);
           // var loggedInUser = _userManager.GetUserAsync(User).Result;
            return View(user);
        }
        public IActionResult ChatWith(string otherId)
        {
            var ThisUser = _userManager.GetUserAsync(User).Result;
            var ThisUserId = ThisUser.Id;
            var allchats = _context.Chats.Include(m=>m.messages).Include(m=>m.OtherUser).Include(m=>m.SenderUser).Where(m => m.SenderUser.Id == ThisUserId ||m.OtherUser.Id==ThisUserId).ToList();
            var chat=_context.Chats.Include(m=>m.messages).Include(m=>m.OtherUser).Where(m=>m.SenderUser.Id == ThisUserId && m.OtherUser.Id==otherId).FirstOrDefault();
            if (chat == null)
            {
             chat=_context.Chats.Include(m=>m.messages).ThenInclude(m=>m.Sender).Include(m=>m.OtherUser).Include(m=>m.SenderUser).Where(m=>m.SenderUser.Id == otherId && m.OtherUser.Id==ThisUserId).FirstOrDefault();

            }
            var viewModel = new ChatViewModel
            {
                Chats = allchats,
       SelectedChat =chat
    };

            return View(viewModel);
        }

        public IActionResult chat(AppUser user,string id,string message)

        {
            if (User.Identity.IsAuthenticated)
            {
                var OtherUserId =id;
                var OtherUser = _context.Users.FirstOrDefault(m => m.Id == OtherUserId);
                var ThisUser = _userManager.GetUserAsync(User).Result;
                var ThisUserId = ThisUser.Id;

                var NewMessage = new Message();
                NewMessage.Sender = ThisUser;
                NewMessage.Reciever = OtherUser;
                NewMessage.message = message;
                _context.Messages.Add(NewMessage);
                _context.SaveChanges();

                var chat = new Chat();
                chat.SenderUser = ThisUser;
                chat.OtherUser= OtherUser;
                chat.messages.Add(NewMessage);
                _context.Chats.Add(chat);
                _context.SaveChanges();
              
                return RedirectToAction(nameof(MyChats));
            }
            return RedirectToAction("Index", "Home");

        }
    }
}



//       ITs A LOGICAL FALLACY

//        A CHAT CANNOT HAVE A SENDER OR A RECIEVER