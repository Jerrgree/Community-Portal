using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
