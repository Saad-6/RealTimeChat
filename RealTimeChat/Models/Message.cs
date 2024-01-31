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
       
        public IdentityUser Sender { get; set; }
      
       
        public IdentityUser Reciever { get; set; }

        public Message()
        {
            DateTime = DateTime.Now;
            Status=Status.Sent;
        
        }


    }
}
