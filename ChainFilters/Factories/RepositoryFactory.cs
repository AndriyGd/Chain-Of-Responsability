using ChainFilters.Model.DataBase;

namespace ChainFilters.Factories
{
    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface IRepositoryFactory
    {
        ICustomerRepository CustomerRepository { get; }
        ICityRepository CityRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStatusItemRepository OrderStatusItemRepository { get; }
    }
}
