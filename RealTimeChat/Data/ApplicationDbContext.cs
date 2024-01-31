using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Models;

namespace RealTimeChat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppUser> Users {  get; set; }
        public DbSet<Message> Messages {  get; set; }
        public DbSet<Chat> Chats {  get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
