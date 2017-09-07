using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChainFilters.Factories;

namespace ChainFilters.ViewModel
{
    using Commands;
    using Model;
    using Services;

    public class MainWindowViewModel
    {
        private readonly OrderFilter _orderFilter;

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderStatusItem> OrderStatusItems { get; set; }
        public List<City> Cities { get; set; }
        public string SelectedStatusItems { get; set; }
        public string SelectedCustomers { get; set; }
        public string SelectedCities { get; set; }
        public BindingList<Order> Orders { get; set; }
        public ICommand SelectedItemCommand { get; set; }

        public MainWindowViewModel(FrameworkElement element)
        {
            _orderFilter = new OrderFilter();
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;

            Orders = new BindingList<Order>();
            OrderStatusItems = FactoryRepositoryFactory.GetFactory().OrderStatusItemRepository.OrderStatusItems;
            Customers = FactoryRepositoryFactory.GetFactory().CustomerRepository.Customers;
            Cities = FactoryRepositoryFactory.GetFactory().CityRepository.Cities;
            SelectedItemCommand = new DeligateCommand(OnSelectedItem, p => OrderStatusItems.Count > 0 && Customers.Count > 0);

            element.DataContext = this;
        }

        private async void OnSelectedItem()
        {
            ProgressTask progress = null;
            try
            {
                progress = new ProgressTask();
                progress.Show();
                Orders.Clear();
                List<byte> statuses = null;
                string[] customers = null;
                string[] cities = null;

                if (!string.IsNullOrEmpty(SelectedStatusItems))
                {
                    statuses = SelectedStatusItems.Split(',').ToList().Select(st => Convert.ToByte(st)).ToList();
                }

                if (!string.IsNullOrEmpty(SelectedCustomers))
                {
                    customers = SelectedCustomers.Split(',');
                }

                if (!string.IsNullOrEmpty(SelectedCities))
                {
                    cities = SelectedCities.Split(',');
                }

                if (customers != null && customers.Length > 0 && customers.Length < Customers.Count)
                {
                    _orderFilter.Customers.AddRange(customers);
                }
                if (cities != null && cities.Length > 0 && cities.Length < Cities.Count)
                {
                    _orderFilter.Cities.AddRange(cities);
                }
                if (statuses != null && statuses.Count > 0 && statuses.Count < OrderStatusItems.Count)
                {
                    _orderFilter.OrderStatuses.AddRange(statuses);
                }

                var orders = await _orderFilter.GetOrdersAsync(DateFrom, DateTo);
                _orderFilter.Dispose();

                orders.ForEach(or => Orders.Add(or));
                progress?.Close();
            }
            catch (Exception)
            {
                progress?.Close();
                throw;
            }
        }
    }
}
