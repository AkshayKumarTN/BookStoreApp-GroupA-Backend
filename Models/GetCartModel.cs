// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCartModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Get cart Model 
    /// </summary>
    public class GetCartModel
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
        /// Gets or sets the cart identifier.
        /// </summary>
        /// <value>
        /// The cart identifier.
        /// </value>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the book count.
        /// </summary>
        /// <value>
        /// The book count.
        /// </value>
        public int BookCount { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        /// <value>
        /// The total cost.
        /// </value>
        public int TotalCost { get; set; }
    }
}
