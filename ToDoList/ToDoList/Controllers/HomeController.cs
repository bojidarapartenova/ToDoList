using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IToDoService toDoService;
    public HomeController(ILogger<HomeController> logger, IToDoService toDoService)
    {
        _logger = logger;
        this.toDoService = toDoService;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<ToDoItem> items = await toDoService.GetUserItemsAsync("global");

        return View(items);
    }

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
