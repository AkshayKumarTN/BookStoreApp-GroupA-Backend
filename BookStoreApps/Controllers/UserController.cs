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
        public UserController(IUserManager manager)
        {
            this.manager = manager;
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
                var result = this.manager.Register(userData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Registration Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
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
            try
            {
                var result = this.manager.Login(loginData);
                string tokenString = this.manager.GenerateToken(loginData.EmailId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Token = tokenString, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
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
            try
            {
                var result = this.manager.ResetPassword(resetData);
                if (result)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Reset Successfull!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
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
