using SecureAPI.DAL;
using SecureAPI.Models;
using SecureAPI.Shared;
using SecureAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Processors
{
    public class UserProcessor
    {
        private UserValidator userValidator;
        private User internalUser;

        public UserProcessor() : this (new User()) { }
        public UserProcessor(User user)
        {
            userValidator = new UserValidator(user);
            internalUser = user;
        }

        public User RegisterNewUser(string password)
        {
            userValidator.ValidateNewUserRequest();

            internalUser.CreateDate = DateTime.Now;
            ResetPassword(password);

            internalUser.AccessLevel = AccessLevels.User;

            UserDAL.SaveNewUser(internalUser);

            return internalUser;
        }

        public string ProcessLoginRequest(LoginRequest request)
        {
            if (internalUser.Email != null)
            {
                internalUser = UserDAL.GetUserByEmailAddress(internalUser.Email);
            }
            else
            {
                internalUser = UserDAL.GetUserByUsername(internalUser.Username);
            }

            userValidator.SetValidatingUser(internalUser);
            userValidator.ValidateLoginRequest(request);

            TokenOperator tokenOperator = new TokenOperator();
            return tokenOperator.GetNewTokenString(internalUser);

        }

        public User GetUser(GetUserRequest request)
        {
            internalUser = userValidator.ValidateGetUserRequest(request);

            return internalUser;
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            internalUser = userValidator.ValidatePasswordResetRequest(request);

            ResetPassword(request.NewPassword);

            UserDAL.UpdateUser(internalUser);

        }

        public User UpdateUser(UpdateUserRequest request)
        {
            internalUser = userValidator.ValidateUpdateRequest(request);

            internalUser.FirstName = request.FirstName;
            internalUser.LastName = request.LastName;
            internalUser.Email = request.Email;

            UserDAL.UpdateUser(internalUser);

            return internalUser;
        }

        public void DeleteUser(DeleteUserRequest request)
        {
            internalUser = userValidator.ValidateDeleteUserRequest(request);

            UserDAL.DeleteUser(internalUser);

        }

        private void ResetPassword(string password)
        {
            internalUser.PasswordCreated = DateTime.Now;
            internalUser.PasswordSalt = PasswordOperator.BuildSaltForUser(internalUser);
            internalUser.PasswordHash = PasswordOperator.GetPasswordHash(internalUser, password);
        }

    }
}
