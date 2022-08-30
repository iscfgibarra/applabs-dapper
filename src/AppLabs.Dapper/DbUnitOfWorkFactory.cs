using AppLabs.Dapper.Abstractions;

namespace AppLabs.Dapper
{
    public class UnitOfWorkFactory
    {
        public static IDbUnitOfWork Create(IDbProviderFactory providerFactory)
        {
            var provider = providerFactory.GetDefaultProvider();
            var connection = provider.GetConnection();
            connection.Open();

            var uow = new DbUnitOfWork(connection);
            uow.Provider = provider;
            return uow;
        }

        public static IDbUnitOfWork Create(IDbProviderFactory providerFactory, DbProviderType providerType)
        {
            var provider = providerFactory.GetProvider(providerType);
            var connection = provider.GetConnection();
            connection.Open();

            var uow = new DbUnitOfWork(connection);
            uow.Provider = provider;
            return uow;
        }

        public static IDbUnitOfWork Create(IDbProviderFactory providerFactory, string providerName)
        {
            var provider = providerFactory.GetProvider(providerName);
            var connection =provider.GetConnection();
            connection.Open();

            var uow = new DbUnitOfWork(connection);
            uow.Provider = provider;
            return uow;
        }
    }
}
