using AppLabs.Dapper.Interfaces;
using AppLabs.QueryExpression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AppLabs.Dapper.Test
{
    class ShopsRepository : Repository<Shop>
    {
        public override string GetFindQueryName => base.GetFindQueryName + " WHERE id=@id ";

        public override object GetFindParameters(int id)
        {
            return new { id };
        }
        public ShopsRepository(IProviderFactory providerFactory) : base(providerFactory)
        {
            FromClause = "shops";
            OrderByClause = "id asc";
       
        }
    }
}