// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedBackRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Models;
    using global::Repository.Interface;

    /// <summary>
    /// Feedback repository implements interface
    /// </summary>
    /// <seealso cref="Repository.Interface.IFeedbackRepository" />
    public class FeedBackRepository : IFeedbackRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedBackRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FeedBackRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

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
                if (feedBackData != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddFeedBackData]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", feedBackData.BookId);
                        cmd.Parameters.AddWithValue("@UserName", feedBackData.UserName);
                        cmd.Parameters.AddWithValue("@Rating", feedBackData.Rating);
                        cmd.Parameters.AddWithValue("@Comments", feedBackData.Comment);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Gets the feed back.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Returns List of feedback model
        /// </returns>
        /// <exception cref="System.Exception">
        /// No FeedBack available
        /// or
        /// </exception>
        public List<FeedBackModel> GetFeedBack(int bookId)
        {
            try
            {
                this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (this.connection)
                {
                    this.connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetFeedBack]", this.connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    List<FeedBackModel> feedBackList = new List<FeedBackModel>();
                    while (sqlDataReader.Read())
                    {
                        FeedBackModel feedBackData = new FeedBackModel();
                        feedBackData.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        feedBackData.UserName = sqlDataReader["UserName"].ToString();
                        feedBackData.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        feedBackData.Comment = sqlDataReader["Comments"].ToString();
                        feedBackList.Add(feedBackData);
                    }

                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No FeedBack available");
                    }

                    return feedBackList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}