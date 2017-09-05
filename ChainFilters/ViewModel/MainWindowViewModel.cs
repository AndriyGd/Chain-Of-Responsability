using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainFilters.Factories;

namespace ChainFilters.ViewModel
{
    using Commands;
    using Model;
    using Services;

    public class MainWindowViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderStatusItem> OrderStatusItems { get; set; }
        public List<Customer> SelectedCustomers { get; set; }
        public List<OrderStatusItem> SelectedStatusItems { get; set; }
        public BindingList<Order> Orders { get; set; }
        public ICommand SelectedItemCommand { get; set; }

        public MainWindowViewModel()
        {
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;

            Orders = new BindingList<Order>();
            OrderStatusItems = FactoryRepositoryFactory.GetFactory().OrderStatusItemRepository.OrderStatusItems;
            Customers = FactoryRepositoryFactory.GetFactory().CustomerRepository.Customers;

            SelectedItemCommand = new DeligateCommand(OnSelectedItem, p => OrderStatusItems.Count > 0 && Customers.Count > 0);
        }

        private async void OnSelectedItem()
        {
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
