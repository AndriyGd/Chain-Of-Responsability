using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace ChainFilters.Model
{
    [AddINotifyPropertyChangedInterface]
    public class City : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sendr, e) => {};

        public string Name { get; set; }
    }
}
