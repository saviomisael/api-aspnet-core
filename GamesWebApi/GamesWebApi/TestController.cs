using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi;

public class TestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}