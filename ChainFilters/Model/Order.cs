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

        public string NumberOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public byte OrderStatus { get; set; }
    }
}
