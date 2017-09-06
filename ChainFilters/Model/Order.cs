using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PropertyChanged;

namespace ChainFilters.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        public int Id { get; set; }
        public string NumberOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public City City { get; set; }
        public OrderStatusItem OrderStatus { get; set; }
    }
}
