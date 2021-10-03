// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyOrderRepository.cs" company="Bridgelabz">
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
    /// Order Repository implements Interface
    /// </summary>
    /// <seealso cref="Repository.Interface.IMyOrderRepository" />
    public class MyOrderRepository : IMyOrderRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyOrderRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MyOrderRepository(IConfiguration configuration)
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
        /// Adds the order.
        /// </summary>
        /// <param name="orderData">The order data.</param>
        /// <returns>
        /// Returns get my order model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public GetMyOrdersModel AddOrder(MyOrdersModel orderData)
        {
            try
            {
                if (orderData != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("AddOrder", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", orderData.UserId);
                        cmd.Parameters.AddWithValue("@BookId", orderData.BookId);
                        cmd.Parameters.AddWithValue("@AddressId", orderData.AddressId);
                        cmd.Parameters.AddWithValue("@OrderDate", orderData.OrderDate);
                        cmd.Parameters.AddWithValue("@TotalCost", orderData.TotalCost);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        GetMyOrdersModel getMyOrders = new GetMyOrdersModel();
                        if (sqlDataReader.Read())
                        {
                            getMyOrders.OrderId = Convert.ToInt32(sqlDataReader["OrderId"]);
                        }

                        return getMyOrders;
                    }
                }

                return null;
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
        /// Gets my orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list of get my order
        /// </returns>
        /// <exception cref="System.Exception">
        /// No Orders in MyOrder
        /// or
        /// </exception>
        public List<GetMyOrdersModel> GetMyOrders(int userId)
        {
            try
            {
                this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (this.connection)
                {
                    this.connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMyOrders", this.connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    List<GetMyOrdersModel> orderList = new List<GetMyOrdersModel>();

                    while (sqlDataReader.Read())
                    {
                        GetMyOrdersModel getMyOrders = new GetMyOrdersModel();
                        getMyOrders.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        getMyOrders.OrderId = Convert.ToInt32(sqlDataReader["OrderId"]);
                        getMyOrders.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        getMyOrders.Title = sqlDataReader["Title"].ToString();
                        getMyOrders.AuthorName = sqlDataReader["AuthorName"].ToString();
                        getMyOrders.BookImage = sqlDataReader["BookImage"].ToString();
                        getMyOrders.OrderDate = sqlDataReader["OrderDate"].ToString();
                        getMyOrders.TotalCost = Convert.ToInt32(sqlDataReader["TotalCost"]);
                        orderList.Add(getMyOrders);
                    }

                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("No Orders in MyOrder");
                    }

                    return orderList;
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
