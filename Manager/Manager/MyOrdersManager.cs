// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyOrdersManager.cs" company="Bridgelabz">
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
    public class MyOrdersManager : IMyOrdersManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IMyOrderRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyOrdersManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MyOrdersManager(IMyOrderRepository repository)
        {
            this.repository = repository;
        }


        

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="orderData">The order data.</param>
        /// <returns>
        /// Returns get my order model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
       public List<int> AddOrder(List<MyOrdersModel> orderData)
        {
            try
            {
                return this.repository.AddOrder(orderData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets my orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list of get my order
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public List<GetMyOrdersModel> GetMyOrders(int userId)
        {
            try
            {
                return this.repository.GetMyOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
