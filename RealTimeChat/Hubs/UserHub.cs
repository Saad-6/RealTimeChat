using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Data;
using RealTimeChat.Models;
using static Azure.Core.HttpHeader;

namespace RealTimeChat.Hubs
{
    public class UserHub:Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserHub(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       

        public async Task DynamicSearch(string query)
        {
             var users = await _userManager.Users.Where(u => u.AppUserName.Contains(query)).ToListAsync();
<<<<<<< HEAD
            
=======
            // var users = _context.Users.FirstOrDefault(m => m.AppUserName == query);
           //var  users = await _context.Users.Where(u => u.AppUserName == query).ToListAsync();
>>>>>>> 122d20511a43c7b31552552180044f743114b4bb
            List<string> names = new List<string>();
            
            foreach (var user in users)
            {
                names.Add(user.AppUserName);

            }
            if (query == "")
            {
                names.Clear();
            }
            await Clients.Client(Context.ConnectionId).SendAsync("DisplaySearchResults", names);
        }
        public async Task CheckEmail(string query)
        {
            string taken = "There already exists an account with this email";
            var user1 =  _context.Users.FirstOrDefault(m=>m.UserName == query); 
            
            if(user1 != null) {
                await Clients.Client(Context.ConnectionId).SendAsync("EmailChecked", taken);
            }
            else
            {

            await Clients.Client(Context.ConnectionId).SendAsync("EmailChecked",null);
            }

        }
        public async Task CheckUserName(string query)
        {
            string taken = "There already exists an account with this username";
            var user1 = _context.Users.FirstOrDefault(m => m.AppUserName == query);
            
            if (user1 != null)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("UserNameChecked", taken);
            }
            else
            {

            await Clients.Client(Context.ConnectionId).SendAsync("UserNameChecked", null);
            }
        }

    }
}
