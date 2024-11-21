using System.Data;
using System.Data.SqlClient;
using Dapper;
using NorthwindWithDapper.Models.Dtos;

namespace NorthwindWithDapper.Repositories;

public class CustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentNullException(nameof(configuration), 
                                "DefaultConnection string is not configured");
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public IEnumerable<CustomerDto> GetAll()
    {
        using var connection = CreateConnection();
        connection.Open();
        return connection.Query<CustomerDto>("SELECT * FROM Customers");
    }
}