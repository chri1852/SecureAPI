using SecureAPI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models
{
    public class TokenRequestResponse : BaseResponse
    {
        public string Token { get; set; }

        public TokenRequestResponse() : this(null, SuccessMessages.DefaultMessage, ExceptionMessages.DefaultMessage) { }
        public TokenRequestResponse(string token, string result, string error) : base(result, error)
        {
            Token = token;
        }
    }

    public class UserResponse : BaseResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime CreateDate { get; set; }
        public int AccessLevel { get; set; }

        public UserResponse() : this(new User(), SuccessMessages.DefaultMessage, ExceptionMessages.DefaultMessage) { }
        public UserResponse(User user, string result, string error) : base(result, error)
        {
            if (user != null)
            {
                Id = user.Id;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                Username = user.Username;
                CreateDate = user.CreateDate;
                AccessLevel = (int)user.AccessLevel;
            }
        }
    }
}
