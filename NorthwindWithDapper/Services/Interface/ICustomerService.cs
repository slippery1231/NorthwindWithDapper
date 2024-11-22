using NorthwindWithDapper.Models.Dtos;
using NorthwindWithDapper.Models.ViewModel;

namespace NorthwindWithDapper.Services.Interface;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetCustomerList();

    CustomerDto GetSingleCustomerInfo(string customerId);

    void AddCustomerInfo(CustomerViewModel viewModel);

    void UpdateCustomerInfo(CustomerViewModel viewModel);
}