// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Interface
{
    using Models;

    /// <summary>
    /// Interface for manager function
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns register model</returns>
        RegisterModel Register(RegisterModel userData);

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>returns register model</returns>
        RegisterModel Login(LoginModel loginData);

        /// <summary>
        /// Forget the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns model</returns>
        OTPModel ForgotPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns>Returns true or false</returns>
        bool ResetPassword(ResetPasswordModel resetData);

        /// <summary>
        /// Edits the personal details.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Returns true or false</returns>
        bool EditPersonalDetails(RegisterModel userData);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns true or false</returns>
        string GenerateToken(string email);
    }
}
