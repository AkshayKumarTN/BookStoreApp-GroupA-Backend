// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWishListManager.cs" company="Bridgelabz">
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
    public class MyWishListManager : IMyWishListManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IMyWishListRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyWishListManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MyWishListManager(IMyWishListRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the book to my wish list.
        /// </summary>
        /// <param name="myWishList">My wish list.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns true or false</exception>
        public bool AddBookToMyWishList(MyWishListModel myWishList)
        {
            try
            {
                return this.repository.AddBookToMyWishList(myWishList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the book from my wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns true or false</exception>
        public List<GetWishListModel> GetBookFromMyWishList(int userId)
        {
            try
            {
                return this.repository.GetBookFromMyWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the book from my wish list.
        /// </summary>
        /// <param name="myWishListId">My wish list identifier.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns true or false</exception>
        public bool RemoveBookFromMyWishList(int myWishListId)
        {
            try
            {
                return this.repository.RemoveBookFromMyWishList(myWishListId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
