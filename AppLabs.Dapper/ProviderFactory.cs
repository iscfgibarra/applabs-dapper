using System;
using System.Collections.Generic;
using System.Linq;
using AppLabs.Dapper.Interfaces;

namespace AppLabs.Dapper
{
    public class ProviderFactory: IProviderFactory
    {
        private ProviderType _defaultProviderType;
        private IDbProvider _defaultProvider;

        public ProviderFactory()
        {
            Providers = new List<IDbProvider>();
        }

        public List<IDbProvider> Providers { get; set; }
        

        public IDbProvider GetProvider(ProviderType providerType)
        {
            return Providers.Where(p => p.ProviderType == providerType).FirstOrDefault();
        }

        public IDbProvider GetProvider(string providerName)
        {
            return Providers.Where(p => p.ProviderName == providerName).FirstOrDefault();
        }

        public IDbProvider SetDefaultProvider(ProviderType providerType)
        {
            _defaultProviderType = providerType;
            return GetDefaultProvider();
        }

        public IDbProvider SetDefaultProvider(string providerName)
        {
            _defaultProvider = Providers.Where(p => p.ProviderName == providerName).FirstOrDefault();
            return _defaultProvider;
        }

        public IDbProvider GetDefaultProvider()
        {
            if (_defaultProvider != null) return _defaultProvider;
            return Providers.Where(p => p.ProviderType == _defaultProviderType).FirstOrDefault();
        }

        public IDbProvider AddProvider(IDbProvider provider)
        {
            if(_defaultProviderType != provider.ProviderType)
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
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
