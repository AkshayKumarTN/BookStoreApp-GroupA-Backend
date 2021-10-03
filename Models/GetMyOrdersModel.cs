// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMyOrdersModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Get My Orders Model
    /// </summary>
    public class GetMyOrdersModel
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public int OrderId { get; set; }

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
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public string OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        /// <value>
        /// The total cost.
        /// </value>
        public int TotalCost { get; set; }

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
        /// Gets or sets the book image.
        /// </summary>
        /// <value>
        /// The book image.
        /// </value>
        public string BookImage { get; set; }
    }
}
