using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Services
{
    public class FactoryRepositoryFactory
    {
        public static IRepositoryFactory GetFactory()
        {
            return InternalRepositoryFactory.Factory;
        }
    }
}
