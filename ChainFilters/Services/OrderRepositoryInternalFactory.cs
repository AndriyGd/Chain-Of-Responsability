using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainFilters.Model.DataBase;

namespace ChainFilters.Services
{
    public class OrderRepositoryInternalFactory : IOrderRepositoryFactory
    {
        private static OrderRepositoryInternalFactory _factory;
        private static readonly object Locker = new object();

        private OrderRepositoryInternal _orderRepository;

        public static OrderRepositoryInternalFactory Factory
        {
            get
            {
                lock (Locker)
                {
                    return _factory ?? (_factory = new OrderRepositoryInternalFactory());
                }
            }
        }

        private OrderRepositoryInternalFactory() {}

        public IOrderRepository Repository
            => _orderRepository ?? (_orderRepository = new OrderRepositoryInternal());
    }
}