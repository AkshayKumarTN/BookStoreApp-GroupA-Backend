// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyOrdersController.cs" company="Bridgelabz">
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
    /// Controller for My Orders
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MyOrdersController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IMyOrdersManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyOrdersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public MyOrdersController(IMyOrdersManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="orderData">The order data.</param>
        /// <returns>Return message and Status and Order Id</returns>
        [HttpPost]
        [Route("Order")]
        public IActionResult AddOrder([FromBody] MyOrdersModel orderData)
        {
            try
            {
              var result = this.manager.AddOrder(orderData);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Added Successfully", result.OrderId });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Order Not Added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets my orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return message and Status</returns>
        [HttpGet]
        [Route("Orders")]
        public IActionResult GetMyOrders(int userId)
        {
            try
            {
                var result = this.manager.GetMyOrders(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetMyOrdersModel>>() { Status = true, Message = "Books Retrived Successfull!!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Added Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
