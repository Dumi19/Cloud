using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class UserDom
    {
        public UserDom()
        {
        }
        public UserDom(string email, string passWord)
        {
            this.email = email;
            this.password = passWord;
        }
         public string email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password should contain 8 characters!")]
        public string password { get; set; }
        public override bool Equals(object obj)
        {
            return obj is UserDom user &&
                   email == user.email &&
                   password == user.password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(email, password);
        }
    }
}
