using AutoMapper;
using NorthwindWithDapper.ExceptionHandler;
using NorthwindWithDapper.Models.Dtos;
using NorthwindWithDapper.Models.Entities;
using NorthwindWithDapper.Models.ViewModel;
using NorthwindWithDapper.Repositories;
using NorthwindWithDapper.Services.Interface;

namespace NorthwindWithDapper.Services.Implement;

public class CustomerService : ICustomerService
{
    private readonly CustomerRepository _dbRepository;
    private readonly IMapper _mapper;

    public CustomerService(CustomerRepository customerRepository, IMapper mapper)
    {
        _dbRepository = customerRepository;
        _mapper = mapper;
    }

    public IEnumerable<CustomerDto> GetCustomerList()
    {
        var data = _dbRepository.GetAll();

        return _mapper.Map<IEnumerable<CustomerDto>>(data).ToList();
    }

    public CustomerDto GetSingleCustomerInfo(string customerId)
    {
        var customer = _dbRepository.GetCustomerById(customerId);

        if (customer == null)
        {
            throw new CustomerNotFoundException("customer is not found");
        }

        return _mapper.Map<CustomerDto>(customer);
    }

    public void AddCustomerInfo(CustomerViewModel viewModel)
    {
        if (_dbRepository.GetCustomerById(viewModel.CustomerId) != null)
        {
            throw new CustomerNotFoundException("customer has already existed");
        }

        var customer = _mapper.Map<Customers>(viewModel);

        _dbRepository.InsertCustomer(customer);
    }

    public void UpdateCustomerInfo(CustomerViewModel viewModel)
    {
        if (_dbRepository.GetCustomerById(viewModel.CustomerId) == null)
        {
            throw new CustomerNotFoundException("customer is not exist");
        }

        var customer = _mapper.Map<Customers>(viewModel);

        _dbRepository.UpdateCustomer(customer);
    }

    public void DeleteCustomerInfo(string customerId)
    {
        var customer = _dbRepository.GetCustomerById(customerId);
        if (customer == null)
        {
            throw new CustomerNotFoundException("customer is not exist");
        }

        var map = _mapper.Map<CustomerDto>(customer);

        _dbRepository.DeleteCustomer(map);
    }
}