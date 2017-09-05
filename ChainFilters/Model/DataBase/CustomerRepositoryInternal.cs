using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Model.DataBase
{
    public class CustomerRepositoryInternal : ICustomerRepository
    {
        private List<Customer> _customers;

        public List<Customer> Customers => _customers ?? (_customers = GetCustomers());

        private static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer {CompanyName = "Sigma"},
                new Customer {CompanyName = "Liskon"},
                new Customer {CompanyName = "ComDir"},
                new Customer {CompanyName = "Colm"},
                new Customer {CompanyName = "RegL"},
                new Customer {CompanyName = "HotK"},
                new Customer {CompanyName = "Mob"},
                new Customer {CompanyName = "ZenDef"},
                new Customer {CompanyName = "RopJ"},
                new Customer {CompanyName = "Lopper"},
                new Customer {CompanyName = "Osran"},
                new Customer {CompanyName = "Iwee"},
                new Customer {CompanyName = "Hamony"}
            };

            return customers;
        }
    }
}
