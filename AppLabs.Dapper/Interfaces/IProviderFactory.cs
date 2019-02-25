using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;

namespace AppLabs.Dapper.Interfaces
{
    public interface IProviderFactory
    {       
        List<IDbProvider> Providers { get; set; }        
        IDbProvider GetProvider(ProviderType providerType);
        IDbProvider GetProvider(string providerName);
        IDbProvider SetDefaultProvider(ProviderType providerType);
        IDbProvider SetDefaultProvider(string providerName);
        IDbProvider GetDefaultProvider();
        IDbProvider AddProvider(IDbProvider provider);
        bool RemoveProvider(IDbProvider provider);
    }
}
