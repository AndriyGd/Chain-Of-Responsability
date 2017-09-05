using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChainFilters.ViewModel
{
    using Model;

    public class MainWindowViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderStatusItem> OrderStatusItems { get; set; }
        public List<Customer> SelectedCustomers { get; set; }
        public List<OrderStatusItem> SelectedStatusItems { get; set; }
        public ICommand SelectedItemCommand { get; set; }

        public MainWindowViewModel()
        {
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
        }
    }
}
