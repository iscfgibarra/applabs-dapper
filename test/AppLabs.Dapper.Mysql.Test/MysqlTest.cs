using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppLabs.Dapper.Test;
using AppLabs.QueryExpression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppLabs.Dapper.Mysql.Test
{
    [TestClass]
    public class MysqlTest
    {
        ProviderFactory _providerFactory;
        ShopsRepository _shopsRepository;


        [TestInitialize]
        public void IniciarFactory()
        {
            _providerFactory = new ProviderFactory();
            _providerFactory.AddProvider(new MySqlProvider());
            _shopsRepository = new ShopsRepository(_providerFactory);
        }

        [TestMethod]
        public async Task GetShops()
        {
           var shops = (List<Shop>) await _shopsRepository.AllAsync();

            Assert.IsTrue(shops.Count == 2);
        }

        [TestMethod]
        public async Task GetIdShop()
        {
            var shop = await _shopsRepository.FindAsync(1);

            Assert.IsTrue(shop.Id == 1);
        }

        [TestMethod]
        public async Task GetWhereShop()
        {
            var where = new WrapperExpression("Name", Operators.Contains, "Bimbo");

            var shops = (List<Shop>)await _shopsRepository.FindAsync(where);

            Assert.IsTrue(shops[0].Name.Contains("Bimbo"));
        }



    }
}
