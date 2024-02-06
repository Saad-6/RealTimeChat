using Microsoft.AspNetCore.Identity;
using RealTimeChat.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? message { get; set; }

        public DateTime DateTime { get; set; }

        public Status Status { get; set; }
       
<<<<<<< HEAD
        public AppUser Sender { get; set; }
      
       
        public AppUser Reciever { get; set; }
=======
        public IdentityUser Sender { get; set; }
      
       
        public IdentityUser Reciever { get; set; }
>>>>>>> 122d20511a43c7b31552552180044f743114b4bb

        public Message()
        {
            DateTime = DateTime.Now;
            Status=Status.Sent;
        
        }


    }
}
