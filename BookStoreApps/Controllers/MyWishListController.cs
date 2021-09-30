﻿using Manager.Interface;
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
    public class MyWishListController : ControllerBase
    {
        private readonly IMyWishListManager manager;

        public MyWishListController(IMyWishListManager manager)
        {
            this.manager = manager;
        }
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

    }
}