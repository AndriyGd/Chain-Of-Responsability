using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Model.DataBase
{
    public class OrderRepositoryInternal : IOrderRepository
    {
        private  List<Order> _orders;

        public List<Order> Orders => _orders ?? (_orders = GetOrders(100));


        private static List<Order> GetOrders(int countOrders)
        {
            try
            {
                var rn = new Random();
                var rn2 = new Random();
                var rn3 = new Random();
                var rn4= new Random();
                var rn5 = new Random();
                var customers = new [] {"Smiga", "Liskon", "ComDir", "Colm", "RegL", "HotK", "Mob", "ZenDef", "RopJ", "Lopper", "Osran", "Iwee", "Hamony"};
               
                var orders = new List<Order>();

                for (var i = 0; i < countOrders; i++)
                {
                    orders.Add(new Order
                    {
                        Id = i + 1,
                        Customer = customers[rn.Next(customers.Length)],
                        NumberOrder = $"A0{rn2.Next()}",
                        OrderDate = new DateTime(2017, rn3.Next(13), rn4.Next(31)),
                        OrderStatus = (byte)rn5.Next(6)                      
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
