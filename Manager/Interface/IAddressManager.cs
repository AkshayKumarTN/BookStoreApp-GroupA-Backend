// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddressManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for manager function
    /// </summary>
    public interface IAddressManager
    {
        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <returns>Returns User Address model</returns>
        UserAddress AddAddress(UserAddress userAddress);

        /// <summary>
        /// Gets all user address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns user address model list</returns>
        public List<UserAddress> GetAllUserAddress(int userId);

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="updateData">The update data.</param>
        /// <returns>Returns User Address model</returns>
        UserAddress UpdateAddress(UserAddress updateData);
    }
}
