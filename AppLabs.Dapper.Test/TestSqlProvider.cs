using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AppLabs.QueryExpression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppLabs.Dapper.Test
{
    [TestClass]
    public class TestSqlProvider
    {
        private DbProviderFactory _providerFactory;
        private LogRepository _logRepository;

        [TestInitialize]
        public void IniciarFactory()
        {
            _providerFactory = new DbProviderFactory();
            _providerFactory.AddProvider(new SqlProvider());
            _logRepository = new LogRepository(_providerFactory);
        }

        [TestMethod]
        public async Task TraerTodoElLog()
        {
            var logs = await _logRepository.AllAsync();

            Assert.IsTrue(logs.Any());
        }

        [TestMethod]
        public async Task TraerLogPorRfc()
        {
            
            var where = new WrapperExpression("h.RFC", 
                Operators.Equals, 
                "AAQM610917QJA");

            
            var logs = await _logRepository.FindAsync(where);
            Assert.IsTrue(logs.Any());
        }


        [TestMethod]
        public async Task TraerLogPorRfcs()
        {
            var rfc1 = new WrapperExpression("h.RFC",Operators.Equals,
                "AAQM610917QJA");

            var rfc2 = new WrapperExpression("h.RFC", Operators.Equals, 
                "C�T040504890");

            var where = new WrapperExpression(rfc1, Operators.Or, rfc2);

            
            var logs = await _logRepository.FindAsync(where);
            Assert.IsTrue(logs.Any());
        }
    }
}
