using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/test")]

public class TestController: ControllerBase 
{
        private readonly IDataService _dataService;

        public TestController(IDataService dataService)
        {
            _dataService = dataService;
        }

    [HttpGet]
    public IActionResult Get()
    {
        string message = _dataService.TestMethod();
        return Ok(message);
    }
}
