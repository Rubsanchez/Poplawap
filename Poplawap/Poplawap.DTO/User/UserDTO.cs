using System;
using System.Collections.Generic;
using System.Text;

namespace Poplawap.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Verified { get; set; }
        public string County { get; set; }
        public string Password { get; set; }
    }
}
