// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedbackController.cs" company="Bridgelabz">
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
    /// Controller for Feedback
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly IFeedbackManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FeedbackController(IFeedbackManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the feed back.
        /// </summary>
        /// <param name="feedBackData">The feed back data.</param>
        /// <returns>Return Message and Status</returns>
        [HttpPost]
        [Route("FeedBack")]
        public IActionResult AddFeedBack([FromBody] FeedBackModel feedBackData)
        {
            try
            {
                var result = this.manager.AddFeedBack(feedBackData);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "FeedBack Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "FeedBack Not Added Successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the feed back.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>Return Message and Status</returns>
        [HttpGet]
        [Route("FeedBack")]
        public IActionResult GetFeedBack(int bookId)
        {
            try
            {
                var result = this.manager.GetFeedBack(bookId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<FeedBackModel>>() { Status = true, Message = "FeedBack Retrived Successfull!!", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not Retrived Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
