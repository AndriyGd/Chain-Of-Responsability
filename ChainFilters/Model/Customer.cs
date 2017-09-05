using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace ChainFilters.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Customer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        public string CompanyName { get; set; }
    }
}
