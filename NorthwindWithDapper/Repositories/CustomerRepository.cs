using System.Data;
using System.Data.SqlClient;
using Dapper;
using NorthwindWithDapper.Models.Dtos;
using NorthwindWithDapper.Models.Entities;

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

    public CustomerDto GetCustomerById(string customerId)
    {
        using var connection = CreateConnection();
        connection.Open();

        return connection.QueryFirstOrDefault<CustomerDto>("SELECT * FROM Customers WHERE CustomerID = @customerId",
            new { customerId });
    }

    public void InsertCustomer(Customers customer)
    {
        const string sql = @"
        INSERT INTO Customers (
            CustomerID, CompanyName, ContactName, ContactTitle,
            Address, City, Region, PostalCode, Country, Phone, Fax
        ) VALUES (
            @CustomerID, @CompanyName, @ContactName, @ContactTitle,
            @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax
        )";

        Execute(customer, sql);
    }


    public void UpdateCustomer(Customers customer)
    {
        const string sql = @"
       UPDATE Customers 
SET 
    CompanyName = @CompanyName,
    ContactName = @ContactName,
    ContactTitle = @ContactTitle,
    Address = @Address,
    City = @City,
    Region = @Region,
    PostalCode = @PostalCode,
    Country = @Country,
    Phone = @Phone,
    Fax = @Fax
WHERE CustomerID = @CustomerID";

        Execute(customer, sql);
    }

    public void DeleteCustomer(CustomerDto customer)
    {
        const string sql = @"
      DELETE Customers
WHERE CustomerID = @customerId";

        using var connection = CreateConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            connection.Execute(sql, customer, transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private void Execute(Customers customer, string sql)
    {
        using var connection = CreateConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            connection.Execute(sql, customer, transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}