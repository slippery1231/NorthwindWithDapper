using NorthwindWithDapper.Models.Dtos;

namespace NorthwindWithDapper.Services.Interface;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetCustomerList();
}