// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartController.cs" company="Bridgelabz">
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
    /// Controller for Cart
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly ICartManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the book to cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>Return message and status</returns>
        [HttpPost]
        [Route("Cart")]
        public IActionResult AddBookToCart([FromBody] CartModel cartData)
        {
            try
            {
                var result = this.manager.AddBookToCart(cartData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added To Cart Successfully!" });
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

        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return message and status</returns>
        [HttpGet]
        [Route("Cart")]
        public IActionResult GetCart(int userId)
        {
            try
            {
                var result = this.manager.GetCart(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetCartModel>>() { Status = true, Message = "Books Retrived Successfull!!", Data = result });
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

        /// <summary>
        /// Updates the count in cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>Return message and status</returns>
        [HttpPut]
        [Route("Cart")]
        public IActionResult UpdateCountInCart(CartModel cartData)
        {
            try
            {
                var result = this.manager.UpdateCountInCart(cartData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Count Decreased!!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Can't be Decreased!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes from cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Return message and status</returns>
        [HttpDelete]
        [Route("Cart")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                var result = this.manager.RemoveBookFromCart(cartId);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Deleted from cart Successfully!!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Deleted from cart Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
