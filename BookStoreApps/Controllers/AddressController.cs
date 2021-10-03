// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace BookStoreApps.Controllers
{
    using System;
    using System.Collections.Generic;
    using Manager.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Controller for Address 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IAddressManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <returns>Return message and Data</returns>
        [HttpPost]
        [Route("Address")]
        public IActionResult AddAddress([FromBody] UserAddress userAddress)
        {
            try
            {
                var result = this.manager.AddAddress(userAddress);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserAddress>() { Status = true, Message = "Address Added Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Adding Address Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all user address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return message and Data and Address List</returns>
        [HttpPost]
        [Route("GetAddress")]
        public IActionResult GetAllUserAddress(int userId)
        {
            try
            {
                var result = this.manager.GetAllUserAddress(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<UserAddress>>() { Status = true, Message = "Address Retrived Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Address Retrived Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="updateData">The update data.</param>
        /// <returns>Return message and Data</returns>
        [HttpPut]
        [Route("Address")]
        public IActionResult UpdateAddress([FromBody] UserAddress updateData)
        {
            try
            {
                var result = this.manager.UpdateAddress(updateData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserAddress>() { Status = true, Message = "Update Address Successfull!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Update Address Unsuccessfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
