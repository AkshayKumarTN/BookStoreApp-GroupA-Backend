// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Manager
{
    using System;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Implements interface
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// returns register model
        /// </returns>
        /// <exception cref="System.Exception">Returns register model</exception>
        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>
        /// returns register model
        /// </returns>
        /// <exception cref="System.Exception">Returns register model</exception>
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                return this.repository.Login(loginData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forget the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// returns model
        /// </returns>
        /// <exception cref="System.Exception">returns model</exception>
        public OTPModel ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Return true or false</exception>
        public bool ResetPassword(ResetPasswordModel resetData)
        {
            try
            {
                return this.repository.ResetPassword(resetData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the personal details.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns true or false</exception>
        public bool EditPersonalDetails(RegisterModel userData)
        {
            try
            {
                return this.repository.EditPersonalDetails(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">return string</exception>
        public string GenerateToken(string email)
        {
            try
            {
                // Send userdata to Repository and return result true or false
                return this.repository.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
