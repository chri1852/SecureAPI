using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SecureAPI.DAL;
using SecureAPI.Models;
using SecureAPI.Shared;
using SecureAPI.Validators;
using SecureAPI.Processors;

namespace SecureAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        
        // GET api/Users/5
        [HttpGet("{id}")]
        public UserResponse Get(int id, string password)
        {
            GetUserRequest request = new GetUserRequest()
            {
                Id = id,
                Token = Request.Headers.Where(h => h.Key == "Authorization").First().Value
            };

            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());

            try
            {
                return new UserResponse(userProcessor.GetUser(request), SuccessMessages.Success, null);
            }
            catch (AuthenticationException ae)
            {
                return new UserResponse(null, FailureMessages.Failure, ae.Message);
            }
            catch (UserValidationException ue)
            {
                return new UserResponse(null, FailureMessages.Failure, ue.Message);
            }
            catch (Exception)
            {
                return new UserResponse(null, FailureMessages.Failure, ExceptionMessages.ServerFailure);
            }

        }
        

        // POST api/Users/Login
        [HttpPost("Login")]
        public TokenRequestResponse LoginUser([FromBody]JObject value)
        {
            LoginRequest request = value.ToObject<LoginRequest>();
            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());

            try
            {
                return new TokenRequestResponse(userProcessor.ProcessLoginRequest(request), SuccessMessages.LoginSuccess, null);
            }
            catch (UserValidationException uve)
            {
                return new TokenRequestResponse(null, FailureMessages.LoginFailed, uve.Message);
            }
            catch (Exception)
            {
                return new TokenRequestResponse(null, FailureMessages.LoginFailed, ExceptionMessages.ServerFailure);
            }
        }

        // POST api/Users/Register
        [HttpPost("Register")]
        public UserResponse RegisterUser([FromBody]JObject value)
        {
            NewUserRequest request = value.ToObject<NewUserRequest>();
            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());
            User newUser;
            try
            {
                newUser = userProcessor.RegisterNewUser(request.Password);
            }
            catch (UserValidationException uve)
            {
                return new UserResponse(null, FailureMessages.UserCreateFailed, uve.Message); 
            }  
            catch (Exception)
            {
                return new UserResponse(null, FailureMessages.UserCreateFailed, ExceptionMessages.ServerFailure);
            }

            return new UserResponse(newUser, SuccessMessages.UserSuccessfullyCreated, null);
        }

        // Put api/Users/ResetPassword
        [HttpPut("ResetPassword")]
        public BaseResponse ResetPassword([FromBody]JObject value)
        {
            ResetPasswordRequest request = value.ToObject<ResetPasswordRequest>();
            request.Token = Request.Headers.Where(h => h.Key == "Authorization").First().Value;

            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());

            try
            {
                userProcessor.ResetPassword(request);
            }
            catch (AuthenticationException ae)
            {
                return new BaseResponse(FailureMessages.PasswordResetFailed, ae.Message);
            }
            catch (UserValidationException ue)
            {
                return new BaseResponse(FailureMessages.PasswordResetFailed, ue.Message);
            }
            catch (Exception)
            {
                return new BaseResponse(FailureMessages.PasswordResetFailed, ExceptionMessages.ServerFailure);
            }

            return new BaseResponse(SuccessMessages.PasswordResetSuccess, null);
        }

        // PUT api/Users
        [HttpPut]
        public UserResponse UpdateUser([FromBody]JObject value)
        {
            UpdateUserRequest request = value.ToObject<UpdateUserRequest>();
            request.Token = Request.Headers.Where(h => h.Key == "Authorization").First().Value;

            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());

            try
            {
                return new UserResponse(userProcessor.UpdateUser(request), SuccessMessages.Success, null);
            }
            catch (AuthenticationException ae)
            {
                return new UserResponse(null, FailureMessages.Failure, ae.Message);
            }
            catch (UserValidationException ue)
            {
                return new UserResponse(null, FailureMessages.Failure, ue.Message);
            }
            catch (Exception)
            {
                return new UserResponse(null, FailureMessages.Failure, ExceptionMessages.ServerFailure);
            }
        }

        // DELETE api/Users/5

        [HttpDelete("{id}")]
        public BaseResponse Delete(int id)
        {
            DeleteUserRequest request = new DeleteUserRequest()
            {
                Id = id,
                Token = Request.Headers.Where(h => h.Key == "Authorization").First().Value
            };

            UserProcessor userProcessor = new UserProcessor(request.ConvertToUser());

            try
            {
                userProcessor.DeleteUser(request);

                return new BaseResponse(SuccessMessages.Success, null);
            }
            catch (AuthenticationException ae)
            {
                return new BaseResponse(FailureMessages.Failure, ae.Message);
            }
            catch (UserValidationException ue)
            {
                return new BaseResponse(FailureMessages.Failure, ue.Message);
            }
            catch (Exception)
            {
                return new BaseResponse(FailureMessages.Failure, ExceptionMessages.ServerFailure);
            }
        }

    }
}