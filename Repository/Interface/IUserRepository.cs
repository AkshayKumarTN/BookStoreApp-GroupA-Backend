// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using Models;

    /// <summary>
    /// User  interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Return </returns>
        public RegisterModel Register(RegisterModel userData);

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns></returns>
        RegisterModel Login(LoginModel loginData);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        OTPModel ForgotPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns></returns>
        bool ResetPassword(ResetPasswordModel resetData);

        /// <summary>
        /// Edits the personal details.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns></returns>
        bool EditPersonalDetails(RegisterModel userData);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        string GenerateToken(string email);
    }
}
