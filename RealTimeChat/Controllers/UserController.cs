using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Data;
using RealTimeChat.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RealTimeChat.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index","Home");
            }
            var loggedInUser = _userManager.GetUserAsync(User).Result;

            return View(loggedInUser);
        }

        [HttpGet("/User/GetUser/{Username}")]
        public async Task<IActionResult> GetUser(string Username)
        {
            var user = _context.Users.FirstOrDefault(m => m.AppUserName == Username);

            return View(user);
        }

       
    }
}
