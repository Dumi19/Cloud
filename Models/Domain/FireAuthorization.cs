using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class FireAuthorization
    {
        public string localId { get; set; }
        public string email { get; set; }
        public string idToken { get; set; }
    }
}
