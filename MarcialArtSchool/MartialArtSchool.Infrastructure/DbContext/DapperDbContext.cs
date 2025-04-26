using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MartialArtSchool.Infrastructure.DbContext;
public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string? connectionString = _configuration.GetConnectionString("SqlConnection");
        _connection = new SqlConnection(connectionString);

    }
    public IDbConnection DbConnection => _connection;
}


