// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Feed Back Model 
    /// </summary>
    public class FeedBackModel
    {
        /// <summary>
        /// Gets or sets the feed back identifier.
        /// </summary>
        /// <value>
        /// The feed back identifier.
        /// </value>
        public int FeedBackId { get; set; }

        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }
    }
}
