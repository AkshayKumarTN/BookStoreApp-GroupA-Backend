// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeedbackManager.cs" company="Bridgelabz">
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
    public interface IFeedbackManager
    {
        /// <summary>
        /// Adds the feed back.
        /// </summary>
        /// <param name="feedBackData">The feed back data.</param>
        /// <returns>Returns true or false</returns>
        bool AddFeedBack(FeedBackModel feedBackData);

        /// <summary>
        /// Gets the feed back.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>Returns feedback model</returns>
        List<FeedBackModel> GetFeedBack(int bookId);
    }
}
