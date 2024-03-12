using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Planner_Application.Models;
using static Planner_Application.OpenWeatherAPI;

namespace Planner_Application.Controllers;

//this class is the Home controller which helps load the home and privacy page
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //gets home page
    public IActionResult Index()
    {
        return View();
    }

    //gets privacy page
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
