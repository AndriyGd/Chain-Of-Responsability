using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainFilters.Factories;
using ChainFilters.Services;

namespace ChainFilters.Model.DataBase
{
    public class OrderRepositoryInternal : IOrderRepository
    {
        private  List<Order> _orders;

        public List<Order> Orders => _orders ?? (_orders = GetOrders(600));


        private static List<Order> GetOrders(int countOrders)
        {
            try
            {
                var rn = new Random();
                var rn2 = new Random();
                var rn3 = new Random();
                var rn4= new Random();
                var rn5 = new Random();
               
                var orders = new List<Order>();

                for (var i = 0; i < countOrders; i++)
                {
                    orders.Add(new Order
                    {
                        Id = i + 1,
                        Customer = FactoryRepositoryFactory.GetFactory().CustomerRepository.Customers[rn.Next(FactoryRepositoryFactory.GetFactory().CustomerRepository.Customers.Count)],
                        NumberOrder = $"A0{rn2.Next()}",
                        OrderDate = new DateTime(2017, rn3.Next(1, 12), rn4.Next(1, 31)),
                        OrderStatus = FactoryRepositoryFactory.GetFactory().OrderStatusItemRepository.OrderStatusItems[rn5.Next(FactoryRepositoryFactory.GetFactory().OrderStatusItemRepository.OrderStatusItems.Count)].Status                     
                    });
                }

                return orders;
            }
            catch (Exception)
            {
                
                throw;
            }   
        }

    }
}
