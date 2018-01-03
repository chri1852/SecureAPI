using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models
{
    public class BaseRequest
    {
        public string Token { get; set; }

        public BaseRequest() : this( null) { }
        public BaseRequest(string token)
        {
            this.Token = token;
        }
    }
}
