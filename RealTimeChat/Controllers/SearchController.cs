using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Data;
using RealTimeChat.Models;

namespace RealTimeChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public SearchController(UserManager<AppUser>userManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public async Task<IActionResult> SearchUsers(string query)
        {
            // Perform a search based on the query
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(query))
                .Select(u => new { u.UserName })
                .ToListAsync();

            return Ok(users);
        }


    }
}
