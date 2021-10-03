// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookController.cs" company="Bridgelabz">
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
    /// Controller for book API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IBookManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>Return status and success message</returns>
        [HttpPost]
        [Route("Book")]
        public IActionResult AddBook([FromForm] AddBookModel bookData)
        {
            try
            {
                var result = this.manager.AddBook(bookData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book Added Successfully!" });
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
        /// Updates the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>Returns status and message</returns>
        [HttpPut]
      [Route("Book")]
        public IActionResult UpdateBook([FromForm] AddBookModel bookData)
        {
            try
            {
                var result = this.manager.UpdateBook(bookData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book Updated Successfully!" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Updated Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <returns>Returns status and message and book List</returns>
        [HttpGet]
        [Route("Books")]
        public IActionResult GetBooks()
        {
            try
            {
                var result = this.manager.GetBooks();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<BookModel>>() { Status = true, Message = "Books Retrived Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Books Retrived Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
