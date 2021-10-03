// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Implements interface
    /// </summary>
    /// <seealso cref="Manager.Interface.IAddressManager" />
    public class AddressManager : IAddressManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IAddressRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <returns>
        /// Returns User Address model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public UserAddress AddAddress(UserAddress userAddress)
        {
            try
            {
                return this.repository.AddAddress(userAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all user address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns user address model list
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public List<UserAddress> GetAllUserAddress(int userId)
        {
            try
            {
                return this.repository.GetAllUserAddress(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="updateData">The update data.</param>
        /// <returns>
        /// Returns User Address model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public UserAddress UpdateAddress(UserAddress updateData)
        {
            try
            {
                return this.repository.UpdateAddress(updateData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
