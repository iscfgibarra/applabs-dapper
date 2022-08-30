using System.Collections.Generic;

namespace AppLabs.Dapper.Abstractions;

public interface IDbProviderFactory
{
    List<IDbProvider> Providers { get; set; }
    IDbProvider GetProvider(DbProviderType providerType);
    IDbProvider GetProvider(string providerName);
    IDbProvider SetDefaultProvider(DbProviderType providerType);
    IDbProvider SetDefaultProvider(string providerName);
    IDbProvider GetDefaultProvider();
    IDbProvider AddProvider(IDbProvider provider);
    bool RemoveProvider(IDbProvider provider);
}