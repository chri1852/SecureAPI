using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using SecureAPI.Shared;

namespace SecureAPI.Models
{
    public class NewUserRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username
            };

            return returnUser;
            
        }
    }

    public class LoginRequest : BaseRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User();

            if(Regex.IsMatch(this.UsernameOrEmail, @".+\@.+"))
            {
                returnUser.Email = UsernameOrEmail;
            }
            else
            {
                returnUser.Username = UsernameOrEmail;
            }

            return returnUser;
        }
    }

    public class UpdateUserRequest : BaseRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AccessLevel { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                AccessLevel = (AccessLevels)AccessLevel

            };

            return returnUser;
        }
    }

    public class ResetPasswordRequest : BaseRequest
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User()
            {
                Id = Id
            };

            return returnUser;
        }
    }

    public class DeleteUserRequest : BaseRequest
    {
        public int Id { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User()
            {
                Id = Id
            };
            return returnUser;
        }
    }

    public class GetUserRequest : BaseRequest
    {
        public int Id { get; set; }

        public User ConvertToUser()
        {
            User returnUser = new User()
            {
                Id = Id
            };
            return returnUser;
        }
    }
}
