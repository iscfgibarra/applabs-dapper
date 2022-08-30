using System;
using System.Collections.Generic;
using System.Linq;
using AppLabs.Dapper.Abstractions;

namespace AppLabs.Dapper;

public class DbProviderFactory : IDbProviderFactory
{
    private DbProviderType _defaultProviderType;
    private IDbProvider _defaultProvider;

    public DbProviderFactory()
    {
        Providers = new List<IDbProvider>();
    }

    public List<IDbProvider> Providers { get; set; }


    public IDbProvider GetProvider(DbProviderType providerType)
    {
        return Providers.FirstOrDefault(p => p.ProviderType == providerType);
    }

    public IDbProvider GetProvider(string providerName)
    {
        return Providers.FirstOrDefault(p => p.ProviderName == providerName);
    }

    public IDbProvider SetDefaultProvider(DbProviderType providerType)
    {
        _defaultProviderType = providerType;
        return GetDefaultProvider();
    }

    public IDbProvider SetDefaultProvider(string providerName)
    {
        _defaultProvider = Providers.FirstOrDefault(p => p.ProviderName == providerName);
        return _defaultProvider;
    }

    public IDbProvider GetDefaultProvider()
    {
        if (_defaultProvider != null) return _defaultProvider;
        return Providers.FirstOrDefault(p => p.ProviderType == _defaultProviderType);
    }

    public IDbProvider AddProvider(IDbProvider provider)
    {
        if (provider != null && DbProviderType.None == provider.ProviderType)
        {
            throw new ArgumentException("El ProviderType no ha sido establecido en la clase que hereda el IDbProvider");
        }

        if (_defaultProviderType != provider.ProviderType)
        {
            _defaultProviderType = provider.ProviderType;
        }
        Providers.Add(provider);
        return provider;
    }

    public bool RemoveProvider(IDbProvider provider)
    {
        try
        {
            Providers.Remove(provider);
            return true;
        }
        catch
        {
            return false;
        }
    }
}