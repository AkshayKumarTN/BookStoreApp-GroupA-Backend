// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWishListController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace BookStoreApps.Controllers
{
    using System;
    using System.Collections.Generic;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Controller for My Wish list
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class MyWishListController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IMyWishListManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyWishListController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public MyWishListController(IMyWishListManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the book to my wish list.
        /// </summary>
        /// <param name="myWishList">My wish list.</param>
        /// <returns>Returns Success Message and status</returns>
        [HttpPost]
        [Route("WishList")]
        public IActionResult AddBookToMyWishList([FromBody] MyWishListModel myWishList)
        {
            try
            {
                var result = this.manager.AddBookToMyWishList(myWishList);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added To myWishList Successfully!" });
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
        /// Gets the book from my wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return Wish List , message and status</returns>
        [HttpGet]
        [Route("WishList")]
        public IActionResult GetBookFromMyWishList(int userId)
        {
            try
            {
                var result = this.manager.GetBookFromMyWishList(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<GetWishListModel>>() { Status = true, Message = "MyWishList Books Retrived Successfully!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "MyWishList Books Retrived UnSuccessfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the book from my wish list.
        /// </summary>
        /// <param name="myWishListId">My wish list identifier.</param>
        /// <returns>Returns Status and message</returns>
        [HttpDelete]
        [Route("WishList")]
        public IActionResult RemoveBookFromMyWishList(int myWishListId)
        {
            try
            {
                var result = this.manager.RemoveBookFromMyWishList(myWishListId);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Removed From myWishList Successfully!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Remove Book From myWishList UnSuccessfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
