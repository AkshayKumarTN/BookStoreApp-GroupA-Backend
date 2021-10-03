// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackManager.cs" company="Bridgelabz">
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
    public class FeedBackManager : IFeedbackManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IFeedbackRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedBackManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FeedBackManager(IFeedbackRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the feed back.
        /// </summary>
        /// <param name="feedBackData">The feed back data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool AddFeedBack(FeedBackModel feedBackData)
        {
            try
            {
                return this.repository.AddFeedBack(feedBackData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the feed back.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Returns feedback model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public List<FeedBackModel> GetFeedBack(int bookId)
        {
            try
            {
                return this.repository.GetFeedBack(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
