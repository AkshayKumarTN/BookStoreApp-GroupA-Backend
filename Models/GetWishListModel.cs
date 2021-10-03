// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetWishListModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Get wish list Model
    /// </summary>
    public class GetWishListModel
    {
        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>
        /// The name of the author.
        /// </value>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the book detail.
        /// </summary>
        /// <value>
        /// The book detail.
        /// </value>
        public string BookDetail { get; set; }

        /// <summary>
        /// Gets or sets the book image.
        /// </summary>
        /// <value>
        /// The book image.
        /// </value>
        public string BookImage { get; set; }

        /// <summary>
        /// Gets or sets the big image.
        /// </summary>
        /// <value>
        /// The big image.
        /// </value>
        public string BigImage { get; set; }

        /// <summary>
        /// Gets or sets the book quantity.
        /// </summary>
        /// <value>
        /// The book quantity.
        /// </value>
        public int BookQuantity { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets my wish list identifier.
        /// </summary>
        /// <value>
        /// My wish list identifier.
        /// </value>
        public int MyWishListId { get; set; }
    }
}
