using Microsoft.Extensions.Configuration;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class FeedBackRepository: IFeedbackRepository
    {
        public FeedBackRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        SqlConnection connection;
        public bool AddFeedBack(FeedBackModel feedBackData)
        {
            try
            {
                if (feedBackData != null)
                {
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddFeedBackData]", connection);
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
                connection.Close();
            }
        }
        public List<FeedBackModel> GetFeedBack(int bookId)
        {
            try
            {
                connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetFeedBack]", connection)
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
                connection.Close();
            }
        }


    }
}
