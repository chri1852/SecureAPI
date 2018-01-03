using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models
{
    public class BaseResponse
    {
        public string ResultMessage { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResponse() : this("Default Response", null) { }
        public BaseResponse(string ResultMessage, string ErrorMessage)
        {
            this.ResultMessage = ResultMessage;
            this.ErrorMessage = ErrorMessage;
        }
    }
}
