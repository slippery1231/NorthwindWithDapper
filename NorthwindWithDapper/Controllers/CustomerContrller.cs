using Microsoft.AspNetCore.Mvc;
using NorthwindWithDapper.Services.Interface;

namespace NorthwindWithDapper.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// 取得客戶清單
    /// </summary>
    /// <returns></returns>
    [HttpGet("api/customers")]
    public IActionResult GetCustomerList()
    {
        var customerList = _customerService.GetCustomerList();
        return Ok(customerList);
    }

    /// <summary>
    /// 取得單一客戶資料
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("api/customers/{customerId}")]
    public IActionResult GetSingleCustomerInfo(string customerId)
    {
        var customer = _customerService.GetSingleCustomerInfo(customerId);
        return Ok(customer);
    }
}