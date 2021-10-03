// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICartRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Cart Repository
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Adds the book to cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>Returns true or false</returns>
        bool AddBookToCart(CartModel cartData);

        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns List of get cart model</returns>
        public List<GetCartModel> GetCart(int userId);

        /// <summary>
        /// Updates the count in cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>Returns true or false</returns>
        bool UpdateCountInCart(CartModel cartData);

        /// <summary>
        /// Removes the book from cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>Returns true or false</returns>
        bool RemoveBookFromCart(int cartId);
    }
}
