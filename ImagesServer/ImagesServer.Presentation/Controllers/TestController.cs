using Microsoft.AspNetCore.Mvc;

namespace ImagesServer.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Test()
    {
        return Ok(new { Message = "It's works!" });
    }
}