﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IMyOrdersManager
    {
        List<int> AddOrder(List<MyOrdersModel> orderData);
        List<GetMyOrdersModel> GetMyOrders(int userId);
    }
}
