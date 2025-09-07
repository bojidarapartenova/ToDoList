using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoList.Data;
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

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddItemInputModel inputModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }
            bool result = await toDoService.AddTaskAsync("global", inputModel);

            if (!result)
            {
                return View(inputModel);
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
