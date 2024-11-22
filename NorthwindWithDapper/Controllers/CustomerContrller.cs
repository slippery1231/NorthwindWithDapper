using Microsoft.AspNetCore.Mvc;
using NorthwindWithDapper.Models.ViewModel;
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

    /// <summary>
    /// 新增一筆客戶資料
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("api/customers")]
    public IActionResult AddCustomerInfo([FromBody] CustomerViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { Message = "Required Fields Are Missing", Errors = errors });
        }

        _customerService.AddCustomerInfo(viewModel);
        return Ok(new { StatusCode = 200, Message = "create successfully" });
    }
    
    /// <summary>
    /// 更新單一筆客戶資料
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    
    [HttpPut("api/customers")]
    public IActionResult UpdateCustomerInfo([FromBody] CustomerViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { Message = "Required Fields Are Missing", Errors = errors });
        }

        _customerService.UpdateCustomerInfo(viewModel);
        return Ok(new { StatusCode = 200, Message = "update successfully" });
    }
}