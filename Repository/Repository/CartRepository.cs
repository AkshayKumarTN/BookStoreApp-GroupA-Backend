// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartRepository.cs" company="Bridgelabz">
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
    /// Cart Repository implement interface
    /// </summary>
    /// <seealso cref="Repository.Interface.ICartRepository" />
    public class CartRepository : ICartRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CartRepository(IConfiguration configuration)
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
        /// Adds the book to cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool AddBookToCart(CartModel cartData)
        {
            try
            {
                if (cartData != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[AddBookToCart]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", cartData.UserId);
                        cmd.Parameters.AddWithValue("@BookId", cartData.BookId);
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
        /// Gets the cart.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns List of get cart model
        /// </returns>
        /// <exception cref="System.Exception">
        /// No Books in Cart list
        /// or
        /// </exception>
        public List<GetCartModel> GetCart(int userId)
        {
            try
            {
                this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (this.connection)
                {
                    this.connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetCart]", this.connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    List<GetCartModel> list = new List<GetCartModel>();

                    while (sqlDataReader.Read())
                    {
                        GetCartModel getCartModel = new GetCartModel();
                        getCartModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        getCartModel.Title = sqlDataReader["Title"].ToString();
                        getCartModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        getCartModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                        getCartModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        getCartModel.BookDetail = sqlDataReader["BookDetail"].ToString();
                        getCartModel.BookImage = sqlDataReader["BookImage"].ToString();
                        getCartModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        getCartModel.CartId = Convert.ToInt32(sqlDataReader["CartId"]);
                        getCartModel.BookCount = Convert.ToInt32(sqlDataReader["BookCount"]);
                        getCartModel.TotalCost = Convert.ToInt32(sqlDataReader["TotalCost"]);
                        getCartModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        list.Add(getCartModel);
                    }

                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Books in Cart list");
                    }

                    return list;
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
        /// Updates the count in cart.
        /// </summary>
        /// <param name="cartData">The cart data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool UpdateCountInCart(CartModel cartData)
        {
            try
            {
                if (cartData != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[DecreaseCount]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartId", cartData.CartId);
                        cmd.Parameters.AddWithValue("@BookId", cartData.BookId);
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
        /// Removes the book from cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool RemoveBookFromCart(int cartId)
        {
            try
            {
                if (cartId != 0)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[RemoveBookFromCart]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartId", cartId);
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
