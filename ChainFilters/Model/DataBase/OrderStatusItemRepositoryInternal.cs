using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Model.DataBase
{
    public class OrderStatusItemRepositoryInternal : IOrderStatusItemRepository
    {
        private List<OrderStatusItem> _orderStatusItems;

        public List<OrderStatusItem> OrderStatusItems
            => _orderStatusItems ?? (_orderStatusItems = GetOrderstatusItems());
        private List<OrderStatusItem> GetOrderstatusItems()
        {
            var items = new List<OrderStatusItem>
            {
                new OrderStatusItem {Status = 0, Text = "Paid"},
                new OrderStatusItem {Status = 1, Text = "Reservation"},
                new OrderStatusItem {Status = 2, Text = "Credit"},
                new OrderStatusItem {Status = 3, Text = "Processing"},
                new OrderStatusItem {Status = 4, Text = "Returned"},
            };

            return items;
        }
    }
}
