// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMyOrderRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Wishlist interface
    /// </summary>
    public interface IMyWishListRepository
    {
        /// <summary>
        /// Adds the book to my wish list.
        /// </summary>
        /// <param name="myWishList">My wish list.</param>
        /// <returns></returns>
        bool AddBookToMyWishList(MyWishListModel myWishList);

        /// <summary>
        /// Gets the book from my wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<GetWishListModel> GetBookFromMyWishList(int userId);

        /// <summary>
        /// Removes the book from my wish list.
        /// </summary>
        /// <param name="myWishListId">My wish list identifier.</param>
        /// <returns></returns>
        public bool RemoveBookFromMyWishList(int myWishListId);
    }
}
