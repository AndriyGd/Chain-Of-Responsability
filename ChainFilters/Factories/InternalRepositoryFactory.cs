using ChainFilters.Model.DataBase;

namespace ChainFilters.Factories
{
    /// <summary>
    /// Specific factory as Singleton
    /// </summary>
    internal class InternalRepositoryFactory : IRepositoryFactory
    {
        private static InternalRepositoryFactory _factory;
        private static readonly object Locker = new object();

        private CustomerRepositoryInternal _customerRepository;
        private OrderRepositoryInternal _orderRepository;
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

        public ICustomerRepository CustomerRepository
            => _customerRepository ?? (_customerRepository = new CustomerRepositoryInternal());

        public IOrderRepository OrderRepository
            => _orderRepository ?? (_orderRepository = new OrderRepositoryInternal());

        public IOrderStatusItemRepository OrderStatusItemRepository
            => _orderStatusItemRepository ?? (_orderStatusItemRepository = new OrderStatusItemRepositoryInternal());
    }
}