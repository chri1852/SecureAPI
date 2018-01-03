using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Shared
{
    public enum AccessLevels
    {
        User,
        Admin
    }

    public static class ExceptionMessages
    {
        public static readonly string InvalidEmailAddress = "Invalid Email Address";
        public static readonly string EmailAddressUsed = "Email Address Already Registered";
        public static readonly string UsernameUsed = "Username Already Registered";
        public static readonly string InvalidCredentials = "Invalid Credentials";
        public static readonly string TokenExpired = "Token Has Expired";
        public static readonly string TokenInvalid = "Invalid Token";
        public static readonly string UserDoesNotExist = "User Does Not Exist";
        public static readonly string AccessDenied = "Access Deinied";

        public static readonly string ServerFailure = "500 Internal Server Error";
        public static readonly string DefaultMessage = "Default Message";
    }

    public static class SuccessMessages
    {
        public static readonly string UserSuccessfullyCreated = "Registration Successful";
        public static readonly string LoginSuccess = "Login Successful";
        public static readonly string AuthenticationSuccess = "Authentication Successful";
        public static readonly string PasswordResetSuccess = "Password Reset";

        public static readonly string Success = "Success";
        public static readonly string DefaultMessage = "Default Message";
    }

    public static class FailureMessages
    {
        public static readonly string UserCreateFailed = "Registration Failed";
        public static readonly string LoginFailed = "Login Failed";
        public static readonly string AuthenticationFailed = "Authentication Failed";
        public static readonly string PasswordResetFailed = "Password Reset Failed";

        public static readonly string Failure = "Failure";
        public static readonly string DefaultMessage = "Default Message";
    }
}
