// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace BookStoreApp.Controllers
{
    using System;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;

    /// <summary>
    /// Controller for User API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>

        /// <summary>
        /// instance for logger
        /// </summary>
        private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                this.logger.LogInformation("API For Registration for Accessing Book Store Application");
                var result = this.manager.Register(userData);
                if (result != null)
                {
<<<<<<< HEAD
                    this.logger.LogInformation(userData.FullName + " Is Registered Successfully");
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Registration Successfull!", Data = result });
=======
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Registration Successfull!", Data= result  });
>>>>>>> a3e490a5f9dd183829f6b25fdaf025910a385a43
                }
                else
                {
                    this.logger.LogWarning("Registration Unsuccesfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While Register " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            this.logger.LogInformation("API For Login to access the books");
            try
            {
                this.logger.LogInformation(loginData.EmailId + " Is Logging ");
                var result = this.manager.Login(loginData);
                string tokenString = this.manager.GenerateToken(loginData.EmailId);
                if (result != null)
                {
                    this.logger.LogInformation(loginData.EmailId + " Logged Successfully");
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Token = tokenString, Data = result });
                }
                else
                {
                    this.logger.LogWarning("Login Unsuccessfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While log in " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Forget the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns message and status</returns>
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            this.logger.LogInformation("API For Forgot Password ");
            try
            {
                var result = this.manager.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<OTPModel>() { Status = true, Message = "Please check your email", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Email not Sent" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetData)
        {
            this.logger.LogInformation("API For Reset Passoword");
            try
            {
                this.logger.LogInformation(resetData.UserId + " Is trying to reset the password");
                var result = this.manager.ResetPassword(resetData);
                if (result)
                {
                    this.logger.LogInformation(resetData.UserId + " Reseted Password Successfully");
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Reset Successfull!" });
                }
                else
                {
                    this.logger.LogWarning("Not Reseted Passowrd Successfully");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While in Reset the password " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edits the personal details.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Returns message and status</returns>
        [HttpPut]
        [Route("PersonalDetails")]
        public IActionResult EditPersonalDetails([FromBody] RegisterModel userData)
        {
            try
            {
                var result = this.manager.EditPersonalDetails(userData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Update Personal Details Successfull!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Update Personal Details Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
