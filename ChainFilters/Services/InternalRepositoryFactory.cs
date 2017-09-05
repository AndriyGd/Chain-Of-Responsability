using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainFilters.Model.DataBase;

namespace ChainFilters.Services
{
    /// <summary>
    /// Specific factory 
    /// </summary>
    internal class InternalRepositoryFactory : IRepositoryFactory
    {
        private static InternalRepositoryFactory _factory;
        private static readonly object Locker = new object();

        private OrderRepositoryInternal _orderRepository;
        private CustomerRepositoryInternal _customerRepository;
        private OrderStatusItemRepositoryInternal _orderStatusItemRepository;

        public static InternalRepositoryFactory Factory
        {
            get
            {
                lock (Locker)
                {
                    return _factory ?? (_factory = new InternalRepositoryFactory());
                }
            }
        }

        private InternalRepositoryFactory() {}

        public IOrderRepository OrderRepository
            => _orderRepository ?? (_orderRepository = new OrderRepositoryInternal());

        public ICustomerRepository CustomerRepository
            => _customerRepository ?? (_customerRepository = new CustomerRepositoryInternal());

        public IOrderStatusItemRepository OrderStatusItemRepository
            => _orderStatusItemRepository ?? (_orderStatusItemRepository = new OrderStatusItemRepositoryInternal());
    }
}