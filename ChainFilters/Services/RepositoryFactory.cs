using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Services
{
    using Model.DataBase;

    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface IRepositoryFactory
    {
        IOrderRepository OrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderStatusItemRepository OrderStatusItemRepository { get; }
    }
}
