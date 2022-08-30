using AppLabs.Dapper.Abstractions;
using AppLabs.Dapper.Postgres.Tests.Entity;
using AppLabs.Dapper.Postgres.Tests.Providers;
using AppLabs.Dapper.Postgres.Tests.Repositories;
using AppLabs.QueryExpression;
using FluentAssertions;

namespace AppLabs.Dapper.Postgres.Tests;

public class PostgresqlDapperTests
{
    private readonly IDbProviderFactory _dbProviderFactory;
    private readonly IDbRepository<Shop> _shopsRepository;

    public PostgresqlDapperTests()
    {
        var server = "192.168.100.13"; //localhost,dnsname
        _dbProviderFactory = new DbProviderFactory();
        var postgresConfig = new PosgresqlConfig
        {
            ConnectionString = $"Host={server};Username=dapper_user;Password=test2022;Database=dapper_test"
        };
        var postgresProvider = new DapperTestPostgresqlProvider(postgresConfig);
        _dbProviderFactory.AddProvider(postgresProvider);
        _shopsRepository = new ShopsRepository(_dbProviderFactory);
    }
    
  
    [Fact]
    public async Task GetShops_Test_Ok()
    {
        var shops = (List<Shop>) await _shopsRepository.AllAsync();

        shops.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetIdShop()
    {
        var shop = await _shopsRepository.FindAsync("1");

        shop.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetWhereShop()
    {
        var where = new WrapperExpression("name", Operators.Contains, "Bimbo");
        var shops = (List<Shop>)await _shopsRepository.FindAsync(where);
        shops[0].Name.Should().Contain("Bimbo");
    }
}