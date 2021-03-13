using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.Entities
{
    public class UserRegistration: Base
    {
       // public long _id { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
