// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="Bridgelabz">
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
    /// Address Repository
    /// </summary>
    /// <seealso cref="Repository.Interface.IAddressRepository" />
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AddressRepository(IConfiguration configuration)
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
        /// Adds the address.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <returns>
        /// Returns User Address model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public UserAddress AddAddress(UserAddress userAddress)
        {
            try
            {
                if (userAddress != null)
                {
                    this.connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("AddAddress", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userAddress.UserId);
                        cmd.Parameters.AddWithValue("@Address", userAddress.Address);
                        cmd.Parameters.AddWithValue("@Type", userAddress.Type);
                        cmd.Parameters.AddWithValue("@City", userAddress.City);
                        cmd.Parameters.AddWithValue("@State", userAddress.State);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return userAddress;
                        }

                        return null;
                    }
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all user address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns user address model list
        /// </returns>
        /// <exception cref="System.Exception">
        /// UserId does not Have Address
        /// or
        /// </exception>
        public List<UserAddress> GetAllUserAddress(int userId)
        {
            try
            {
                if (userId != 0)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("GetAllUserAddress", this.connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();

                        List<UserAddress> userAddresseList = new List<UserAddress>();
                        while (sqlDataReader.Read())
                        {
                            UserAddress userAddress = new UserAddress();
                            userAddress.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                            userAddress.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            userAddress.Address = sqlDataReader["Address"].ToString();
                            userAddress.Type = sqlDataReader["Type"].ToString();
                            userAddress.City = sqlDataReader["City"].ToString();
                            userAddress.State = sqlDataReader["State"].ToString();
                            userAddresseList.Add(userAddress);
                        }

                        if (sqlDataReader.HasRows == false)
                        {
                            throw new Exception("UserId does not Have Address");
                        }

                        return userAddresseList;
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
        /// Updates the address.
        /// </summary>
        /// <param name="updateData">The update data.</param>
        /// <returns>
        /// Returns User Address model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public UserAddress UpdateAddress(UserAddress updateData)
        {
            try
            {
                if (updateData != null)
                {
                    this.connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateAddress", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AddressId", updateData.AddressId);
                        cmd.Parameters.AddWithValue("@Address", updateData.Address);
                        cmd.Parameters.AddWithValue("@Type", updateData.Type);
                        cmd.Parameters.AddWithValue("@City", updateData.City);
                        cmd.Parameters.AddWithValue("@State", updateData.State);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return updateData;
                        }

                        return null;
                    }
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
