using System;
using System.Text.RegularExpressions;

using SecureAPI.Shared;
using SecureAPI.Models;
using SecureAPI.DAL;

namespace SecureAPI.Validators
{
    public class UserValidator
    {
        private User internalUser;
        private TokenOperator tokenOperator;

        public UserValidator() : this(new User()) { }
        public UserValidator(User user)
        {
            internalUser = user;
            this.tokenOperator = new TokenOperator();
        }

        public void SetValidatingUser(User user)
        {
            internalUser = user;
        }

        public void ValidateLoginRequest(LoginRequest request)
        {
            if (internalUser == null)
            {
                throw new UserValidationException(ExceptionMessages.InvalidCredentials);
            }

            // At this point the user exists and is loaded in Internal User
            if (!PasswordOperator.ValidatePassword(internalUser, request.Password))
            {
                throw new UserValidationException(ExceptionMessages.InvalidCredentials);
            }
        }

        public User ValidateUpdateRequest(UpdateUserRequest request)
        {
            internalUser = ValidateUserExists();

            User tokenData = tokenOperator.ReadToken(request.Token);

            if (tokenData.AccessLevel != AccessLevels.Admin)
            {
                if (tokenData.Id != internalUser.Id)
                {
                    throw new AuthenticationException(ExceptionMessages.AccessDenied);
                }
            }
            else
            {
                if (internalUser.AccessLevel != (AccessLevels)request.AccessLevel)
                {
                    internalUser.AccessLevel = (AccessLevels)request.AccessLevel;
                }
            }

            if (internalUser.Email != request.Email)
            {
                internalUser.Email = request.Email;

                if (!ValidateEmail())
                {
                    throw new UserValidationException(ExceptionMessages.InvalidEmailAddress);
                }

                if (!ValidateEmailNotRegistered())
                {
                    throw new UserValidationException(ExceptionMessages.EmailAddressUsed);
                }
            }

            return internalUser;
        }

        public User ValidatePasswordResetRequest(ResetPasswordRequest request)
        {
            internalUser = ValidateUserExists();

            User tokenData = tokenOperator.ReadToken(request.Token);

            if (tokenData.AccessLevel != AccessLevels.Admin)
            {
                if (tokenData.Id != internalUser.Id)
                {
                    throw new AuthenticationException(ExceptionMessages.AccessDenied);
                }
                
                if (!PasswordOperator.ValidatePassword(internalUser, request.OldPassword))
                {
                    throw new UserValidationException(ExceptionMessages.InvalidCredentials);
                }
            }

            return internalUser;
        }


        public void ValidateNewUserRequest()
        {
            if (!ValidateEmail())
            {
                throw new UserValidationException(ExceptionMessages.InvalidEmailAddress);
            }

            if (!ValidateEmailNotRegistered())
            {
                throw new UserValidationException(ExceptionMessages.EmailAddressUsed);
            }

            if (!ValidateUsernameNotRegistered())
            {
                throw new UserValidationException(ExceptionMessages.UsernameUsed);
            }

        }

        public User ValidateGetUserRequest(GetUserRequest request)
        {
            internalUser = ValidateUserExists();

            User tokenData = tokenOperator.ReadToken(request.Token);

            if (tokenData.AccessLevel != AccessLevels.Admin)
            {
                if (tokenData.Id != internalUser.Id)
                {
                    throw new AuthenticationException(ExceptionMessages.AccessDenied);
                }
            }

            return internalUser;
        }

        public User ValidateDeleteUserRequest(DeleteUserRequest request)
        {
            internalUser = ValidateUserExists();

            User tokenData = tokenOperator.ReadToken(request.Token);

            if(tokenData.AccessLevel != AccessLevels.Admin)
            {
                if(tokenData.Id != internalUser.Id)
                {
                    throw new AuthenticationException(ExceptionMessages.AccessDenied);
                }
            }

            return internalUser;
        }


        // returns the user if they exist
        private User ValidateUserExists()
        {
            internalUser = UserDAL.GetUserByUserId(internalUser.Id);

            if (internalUser == null)
            {
                throw new UserValidationException(ExceptionMessages.UserDoesNotExist);
            }
            else
            {
                return internalUser;
            }
        }

        private bool ValidateEmail()
        {
            if (Regex.IsMatch(internalUser.Email, @"^([0-9a-zA-Z]" + @"([\+\-_\.][0-9a-zA-Z]+)*" + @")+" + @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$"))
            {
                return true;
            }

            return false;
        }

        private bool ValidateEmailNotRegistered()
        {
            if (UserDAL.GetEmailAddressCount(internalUser.Email) == 0)
            {
                return true;
            }

            return false;
        }

        private bool ValidateUsernameNotRegistered()
        {
            if (UserDAL.GetUsernameCount(internalUser.Username) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
