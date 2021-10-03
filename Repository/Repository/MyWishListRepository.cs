// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWishListRepository.cs" company="Bridgelabz">
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
    /// Wish list repository implement interface
    /// </summary>
    /// <seealso cref="Repository.Interface.IMyWishListRepository" />
    public class MyWishListRepository : IMyWishListRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyWishListRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MyWishListRepository(IConfiguration configuration)
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
        /// Adds the book to my wish list.
        /// </summary>
        /// <param name="myWishList">My wish list.</param>
        /// <returns>Return true or false</returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool AddBookToMyWishList(MyWishListModel myWishList)
        {
            try
            {
                if (myWishList != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddBookToMyWishList]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", myWishList.UserId);
                        cmd.Parameters.AddWithValue("@BookId", myWishList.BookId);
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
        /// Gets the book from my wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns list of get wish list model</returns>
        /// <exception cref="System.Exception">
        /// No Books in MyWishList
        /// or
        /// </exception>
        public List<GetWishListModel> GetBookFromMyWishList(int userId)
        {
            try
            {
                this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (this.connection)
                {
                    this.connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetBookFromMyWishList]", this.connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    List<GetWishListModel> bookList = new List<GetWishListModel>();

                    while (sqlDataReader.Read())
                    {
                        GetWishListModel bookModel = new GetWishListModel();
                        bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        bookModel.Title = sqlDataReader["Title"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        bookModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                        bookModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        bookModel.BookDetail = sqlDataReader["BookDetail"].ToString();
                        bookModel.BookImage = sqlDataReader["BookImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        bookModel.MyWishListId = Convert.ToInt32(sqlDataReader["MyWishListId"]);
                        bookList.Add(bookModel);
                    }

                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Books in MyWishList");
                    }

                    return bookList;
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

        /// <summary>
        /// Removes the book from my wish list.
        /// </summary>
        /// <param name="myWishListId">My wish list identifier.</param>
        /// <returns>Returns true or false if book removed</returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool RemoveBookFromMyWishList(int myWishListId)
        {
            try
            {
                if (myWishListId != 0)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[RemoveBookFromMyWishList]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MyWishListId", myWishListId);
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
    }
}
