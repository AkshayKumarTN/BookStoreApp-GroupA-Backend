using Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApps.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager manager;

        public FeedbackController(IFeedbackManager manager)
        {
            this.manager = manager;
        }
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
