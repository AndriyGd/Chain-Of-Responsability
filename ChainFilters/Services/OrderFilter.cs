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
        public List<string> Cities { get; set; }
        public List<byte> OrderStatuses { get; }
        #endregion

        public OrderFilter()
        {
            Customers = new List<string>();
            Cities = new List<string>();
            OrderStatuses = new List<byte>();

            _orderSelectors = new Stack<OrderSelector>();
            _orderSelectors.Push(new OrderSelectorByStatus(null));
            _orderSelectors.Push(new OrderSelectorByCity(_orderSelectors.Peek()));
            _orderSelectors.Push(new OrderSelectorByCustomer(_orderSelectors.Peek()));
            _orderSelectors.Push(new OrederSelectorByOrderDate(_orderSelectors.Peek()));
        }

        protected int GetCountItems(OrderSelector nextOrderSelector)
        {
            if (nextOrderSelector == null) return 0;

            if (nextOrderSelector is OrderSelectorByStatus)
            {
                return OrderStatuses.Count;
            }

            if (nextOrderSelector is OrderSelectorByCity)
            {
                return Cities.Count;
            }

            if (nextOrderSelector is OrderSelectorByCustomer)
            {
                return Customers.Count;
            }

            return 0;
        }

        public async Task<List<Order>> GetOrdersAsync(DateTime dtFrom, DateTime dtTo)
        {
            var orders = new List<Order>();
            await Task.Run(() =>
            {
                DtFrom = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 0, 0, 0,000);
                DtTo = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day, 23, 59, 59, 999);

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
                if (orderFilter.Customers.Count == 0 && orderFilter.OrderStatuses.Count == 0 && orderFilter.Cities.Count == 0)
                {
                    orders.AddRange(orderFilter.GetDefaultOrders());
                }
                else
                {
                    base.SelectOrders(orderFilter, ref orders);
                }
            }
        }

        protected class OrderSelectorByCustomer : OrderSelector
        {
            private readonly OrderSelector _nextOrderSelector;
            public OrderSelectorByCustomer(OrderSelector nextOrderSelector) : base(nextOrderSelector)
            {
                _nextOrderSelector = nextOrderSelector;
            }

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                if (orders.Count == 0 && orderFilter.GetCountItems(_nextOrderSelector) == 0) //&& orderFilter.OrderStatuses.Count == 0
                {
                    // if orderFilter.Customers.Count == 0 get all orders by OrderDate
                    if (orderFilter.Customers.Count == 0)
                    {
                        orders.AddRange(orderFilter.GetDefaultOrders());
                    }
                    else
                    {
                        orders = GetOrdersByCustomer(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                    }
                }
                else
                {
                    // if Customers.Count == 0 skip select orders. Filter is disabled.
                    if (orderFilter.Customers.Count > 0)
                    {
                        if (orders.Count == 0)
                        {
                            orders = GetOrdersByCustomer(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                        }
                        else
                        {
                            orders = GetOrdersByCustomer(orderFilter, orders);
                        }
                    }                    
                }

                base.SelectOrders(orderFilter, ref orders);
            }

            private static List<Order> GetOrdersByCustomer(OrderFilter orderFilter, IReadOnlyCollection<Order> source)
            {
                var ordersWithCustomers = new List<Order>();
                foreach (var customer in orderFilter.Customers)
                {
                    Thread.Sleep(100);

                    var ords = from or in source
                        where or.Customer.CompanyName.Equals(customer)
                        select or;
                    ordersWithCustomers.AddRange(ords);
                }

                return ordersWithCustomers;
            }
        }

        protected class OrderSelectorByCity : OrderSelector
        {
            private readonly OrderSelector _nextOrderSelector;
            public OrderSelectorByCity(OrderSelector nextOrderSelector) : base(nextOrderSelector)
            {
                _nextOrderSelector = nextOrderSelector;
            }

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {
                if (orders.Count == 0 && orderFilter.GetCountItems(_nextOrderSelector) == 0) //&& orderFilter.OrderStatuses.Count == 0
                {
                    // if orderFilter.Customers.Count == 0 get all orders by OrderDate
                    if (orderFilter.Cities.Count == 0)
                    {
                        orders.AddRange(orderFilter.GetDefaultOrders());
                    }
                    else
                    {
                        orders = GetOrdersByCustomer(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                    }
                }
                else
                {
                    // if Customers.Count == 0 skip select orders. Filter is disabled.
                    if (orderFilter.Cities.Count > 0)
                    {
                        if (orders.Count == 0)
                        {
                            orders = GetOrdersByCustomer(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                        }
                        else
                        {
                            orders = GetOrdersByCustomer(orderFilter, orders);
                        }
                    }
                }

                base.SelectOrders(orderFilter, ref orders);
            }

            private static List<Order> GetOrdersByCustomer(OrderFilter orderFilter, IReadOnlyCollection<Order> source)
            {
                var ordersWithCustomers = new List<Order>();
                foreach (var city in orderFilter.Cities)
                {
                    Thread.Sleep(100);

                    var ords = from or in source
                               where or.City.Name.Equals(city)
                               select or;
                    ordersWithCustomers.AddRange(ords);
                }

                return ordersWithCustomers;
            }
        }

        protected class OrderSelectorByStatus : OrderSelector
        {
            private readonly OrderSelector _nextOrderSelector;
            public OrderSelectorByStatus(OrderSelector nextOrderSelector) : base(nextOrderSelector)
            {
                _nextOrderSelector = nextOrderSelector;
            }

            public override void SelectOrders(OrderFilter orderFilter, ref List<Order> orders)
            {      
                if (orders.Count == 0 && orderFilter.GetCountItems(_nextOrderSelector) == 0) // && orderFilter.Customers.Count == 0
                {
                    // if orderFilter.OrderStatuses.Count == 0 get all orders by OrderDate
                    if (orderFilter.OrderStatuses.Count == 0)
                    {
                        orders.AddRange(orderFilter.GetDefaultOrders());
                    }
                    else
                    {
                        orders = GetOrdersByOrderStatus(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                    }
                }
                else
                {
                    // if OrderStatuses.Count == 0 skip select orders. Filter is disabled.
                    if (orderFilter.OrderStatuses.Count > 0)
                    {
                        if (orders.Count == 0)
                        {
                            orders = GetOrdersByOrderStatus(orderFilter, FactoryRepositoryFactory.GetFactory().OrderRepository.Orders);
                        }
                        else
                        {
                            orders = GetOrdersByOrderStatus(orderFilter, orders);
                        }
                    }
                }
                base.SelectOrders(orderFilter, ref orders);
            }

            private static List<Order> GetOrdersByOrderStatus(OrderFilter orderFilter, IReadOnlyCollection<Order> orders)
            {
                var ordersWithStatus = new List<Order>();
                foreach (var orderStatuse in orderFilter.OrderStatuses)
                {
                    Thread.Sleep(100);

                    var ords = from or in orders
                               where or.OrderStatus.Status.Equals(orderStatuse)
                        select or;
                    ordersWithStatus.AddRange(ords);
                }

                return ordersWithStatus;
            }
        }

        protected IEnumerable<Order> GetDefaultOrders()
        {
            try
            {
                Thread.Sleep(2000);
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
                Cities.Clear();
                OrderStatuses.Clear();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
