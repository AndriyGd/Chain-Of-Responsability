using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChainFilters.Factories;
using ChainFilters.Model;

namespace ChainFilters.Services
{
    public class OrderFilter : IDisposable
    {
        #region privat fields

        private Stack<OrderSelector> _orderSelectors;
        #endregion

        #region protected fields
        protected DateTime DtFrom;
        protected DateTime DtTo;
        #endregion

        #region public fields
        public List<string> Customers { get; }
        public List<byte> OrderStatuses { get; }
        #endregion

        public OrderFilter()
        {
            Customers = new List<string>();
            OrderStatuses = new List<byte>();

            _orderSelectors = new Stack<OrderSelector>();
            _orderSelectors.Push(new OrderSelectorByStatus(null));
            _orderSelectors.Push(new OrderSelectorByCustomer(_orderSelectors.Peek()));
            _orderSelectors.Push(new OrederSelectorByOrderDate(_orderSelectors.Peek()));
        }

        public async Task<List<Order>> GetOrdersAsync(DateTime dtFrom, DateTime dtTo)
        {
            var orders = new List<Order>();
            await Task.Run(() =>
            {
                Dispose();
                DtFrom = dtFrom;
                DtTo = dtTo;

                _orderSelectors.Peek().SelectOrders(this, ref orders);
                orders = orders.OrderByDescending(or => or.OrderDate).ToList();
            });

            return orders;
        }

        protected abstract class OrderSelector
        {
            public OrderSelector NextOrderSelector { get;}

            protected OrderSelector(OrderSelector nextOrderSelector)
            {
                NextOrderSelector = nextOrderSelector;
            }

            public virtual void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                NextOrderSelector?.SelectOrders(orderFilter, ref orders);
            }
        }

        protected class OrederSelectorByOrderDate : OrderSelector
        {
            public OrederSelectorByOrderDate(OrderSelector nextOrderSelector) : base(nextOrderSelector) {}

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                // get all orders by OrderDate
                if (orderFilter.Customers.Count == 0 && orderFilter.OrderStatuses.Count == 0)
                {                   
                    orders.AddRange(orderFilter.GetDefaultOrders());
                    base.SelectOrders(orderFilter, ref orders);
                }
            }
        }

        protected class OrderSelectorByCustomer : OrderSelector
        {
            public OrderSelectorByCustomer(OrderSelector nextOrderSelector) : base(nextOrderSelector) { }

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                // if orders.count == 0 get all orders by OrderDate
                if (orders.Count == 0)
                {
                    orders.AddRange(orderFilter.GetDefaultOrders());
                }
                else
                {
                    // if Customers.Count == 0 skip select orders. Filter is disabled.
                    if (orderFilter.Customers.Count != 0)
                    {
                        var ordersWithCustomers = new List<Order>();
                        foreach (var customer in orderFilter.Customers)
                        {
                            Thread.Sleep(1000);

                            var ords = from or in orders where or.Customer.CompanyName.Equals(customer) select or;
                            ordersWithCustomers.AddRange(ords);
                        }

                        orders = new List<Order>(ordersWithCustomers);
                    }                    
                }

                base.SelectOrders(orderFilter, ref orders);
            }
        }

        protected class OrderSelectorByStatus : OrderSelector
        {
            public OrderSelectorByStatus(OrderSelector nextOrderSelector) : base(nextOrderSelector) { }

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                // if orders.count == 0 get all orders by OrderDate
                if (orders.Count == 0)
                {
                    orders.AddRange(orderFilter.GetDefaultOrders());
                }
                else
                {
                    // if OrderStatuses.Count == 0 skip select orders. Filter is disabled.
                    if (orderFilter.OrderStatuses.Count > 0)
                    {
                        var ordersWithStatus = new List<Order>();
                        foreach (var orderStatuse in orderFilter.OrderStatuses)
                        {
                            var ords = from or in orders where or.OrderStatus.Equals(orderStatuse) select or;
                            ordersWithStatus.AddRange(ords);
                        }

                        orders.AddRange(ordersWithStatus);
                    }
                }
                base.SelectOrders(orderFilter, ref orders);
            }
        }

        protected IEnumerable<Order> GetDefaultOrders()
        {
            try
            {
                Thread.Sleep(3000);
                var ordersWithDate =
                    FactoryRepositoryFactory.GetFactory().OrderRepository.Orders.Where(
                        or => or.OrderDate >= DtFrom && or.OrderDate <= DtTo);

                return ordersWithDate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                Customers.Clear();
                OrderStatuses.Clear();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
