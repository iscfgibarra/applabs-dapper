using AppLabs.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AppLabs.Dapper.Test
{
    class MySqlProvider : IDbProvider
    {
        public MySqlProvider()
        {
            ProviderName = "DbDapperTest";
            ProviderType = ProviderType.MySql;
        }
        public string ProviderName { get; set; }
        public ProviderType ProviderType { get; set; }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        public string GetConnectionString()
        {
            return "Server=localhost;Database=dapper_test;Uid=root;Pwd=;";
        }
    }
}
