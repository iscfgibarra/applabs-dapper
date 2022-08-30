using AppLabs.Dapper.Abstractions;
using AppLabs.Dapper.Postgres.Tests.Entity;

namespace AppLabs.Dapper.Postgres.Tests.Repositories;

public class ShopsRepository : DbRepository<Shop>
{
    public override string GetFindQueryName => base.GetFindQueryName + " WHERE id=@id ";

    public override object GetFindParameters(string id)
    {
        return new { id = int.Parse(id)};
    }
    public ShopsRepository(IDbProviderFactory providerFactory) : base(providerFactory)
    {
        FromClause = "shops";
        OrderByClause = "id asc";
    }
}