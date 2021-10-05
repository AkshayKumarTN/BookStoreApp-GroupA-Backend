// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeedbackRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Feedback repository
    /// </summary>
    public interface IFeedbackRepository
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
        /// <returns>Returns List of feedback model</returns>
        List<FeedBackModel> GetFeedBack(int bookId);
    }
}
