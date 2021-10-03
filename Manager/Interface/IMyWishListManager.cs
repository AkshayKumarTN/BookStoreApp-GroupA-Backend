// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMyWishListManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for manager function
    /// </summary>
    public interface IMyWishListManager
    {
        /// <summary>
        /// Adds the book to my wish list.
        /// </summary>
        /// <param name="myWishList">My wish list.</param>
        /// <returns>Returns true or false</returns>
        bool AddBookToMyWishList(MyWishListModel myWishList);

        /// <summary>
        /// Gets the book from my wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns true or false</returns>
        public List<GetWishListModel> GetBookFromMyWishList(int userId);

        /// <summary>
        /// Removes the book from my wish list.
        /// </summary>
        /// <param name="myWishListId">My wish list identifier.</param>
        /// <returns>Returns true or false</returns>
        public bool RemoveBookFromMyWishList(int myWishListId);
    }
}
