// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartManager.cs" company="Bridgelabz">
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
    public class CartManager : ICartManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICartRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the book to cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool AddBookToCart(CartModel cartData)
        {
            try
            {
                return this.repository.AddBookToCart(cartData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns List of get cart model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public List<GetCartModel> GetCart(int userId)
        {
            try
            {
                return this.repository.GetCart(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the count in cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool UpdateCountInCart(CartModel cartData)
        {
            try
            {
                return this.repository.UpdateCountInCart(cartData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the book from cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool RemoveBookFromCart(int cartId)
        {
            try
            {
                return this.repository.RemoveBookFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
