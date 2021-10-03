// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWishListModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// My wish list Model
    /// </summary>
    public class MyWishListModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets my wish list identifier.
        /// </summary>
        /// <value>
        /// My wish list identifier.
        /// </value>
        public int MyWishListId { get; set; }
    }
}
