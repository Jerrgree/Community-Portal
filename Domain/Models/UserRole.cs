using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
