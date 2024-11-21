using AutoMapper;
using NorthwindWithDapper.Models.Dtos;
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
}