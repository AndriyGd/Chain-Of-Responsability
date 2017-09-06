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
                new OrderStatusItem {Status = 0, Name = "Paid"},
                new OrderStatusItem {Status = 1, Name = "Reservation"},
                new OrderStatusItem {Status = 2, Name = "Credit"},
                new OrderStatusItem {Status = 3, Name = "Processing"},
                new OrderStatusItem {Status = 4, Name = "Returned"},
            };

            return items;
        }
    }
}
