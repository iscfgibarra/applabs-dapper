using AppLabs.Dapper.Interfaces;

namespace AppLabs.Dapper
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create(IProviderFactory providerFactory)
        {
            var connection = providerFactory.GetDefaultProvider().GetConnection();
            connection.Open();

            return new UnitOfWork(connection);
        }

        public static IUnitOfWork Create(IProviderFactory providerFactory, ProviderType providerType)
        {
            var connection = providerFactory.GetProvider(providerType).GetConnection();
            connection.Open();

            return new UnitOfWork(connection);
        }

        public static IUnitOfWork Create(IProviderFactory providerFactory, string providerName)
        {
            var connection = providerFactory.GetProvider(providerName).GetConnection();
            connection.Open();

            return new UnitOfWork(connection);
        }
    }
}
