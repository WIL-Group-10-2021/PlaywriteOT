using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Models
{
    public class UserVM
    {
        // empty constructor
        public UserVM() { }

        public UserVM(string fName, string lName, string email, bool uRole)
        {
            FName = fName;
            LName = lName;
            Email = email;
            Admin = uRole;
        }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
    }
}
