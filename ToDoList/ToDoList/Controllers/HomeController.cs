using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.ViewModels;

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

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            DeleteItemViewModel? itemToDelete = await
            toDoService.GetItemToDeleteAsync(id);

            if (itemToDelete == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(itemToDelete);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteItemViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool result = await toDoService.SoftDeleteItem(viewModel);

            if (result == false)
            {
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            EditItemInputModel? itemToEdit = await
            toDoService.GetItemToEditAsync(id);

            if (itemToEdit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(itemToEdit);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditItemInputModel inputModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {

                return View(inputModel);
            }

            bool result = await toDoService.EditItemAsync(inputModel);

            if (result == false)
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

    [HttpGet]
    public async Task<IActionResult> Pin(int id)
    {
        try
        {
            bool result = await toDoService.PinTaskAsync(id);

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