using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Models
{
    public class User
    {
        // empty constructor
        public User() { }

        public int UId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] UPassword { get; set; }
        public byte[] USalt { get; set; }
        public bool Admin { get; set; }
    }
}
