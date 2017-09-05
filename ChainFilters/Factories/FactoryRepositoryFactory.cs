namespace ChainFilters.Factories
{
    public class FactoryRepositoryFactory
    {
        /// <summary>
        /// Factory method
        /// </summary>
        /// <returns></returns>
        public static IRepositoryFactory GetFactory()
        {
            return InternalRepositoryFactory.Factory;
        }
    }
}
