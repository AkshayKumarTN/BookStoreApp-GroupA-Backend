// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMyOrderRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Order Interface
    /// </summary>
    public interface IMyOrderRepository
    {

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="orderData">The order data.</param>
        /// <returns>Returns get my order model</returns>
       List<int> AddOrder(List<MyOrdersModel> orderData);

        /// <summary>
        /// Gets my orders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list of get my order</returns>
        List<GetMyOrdersModel> GetMyOrders(int userId);
    }
}
