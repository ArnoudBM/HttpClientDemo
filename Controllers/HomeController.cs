using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HttpClientDemo.Clients;
using HttpClientDemo.Models;

namespace HttpClientDemo.Controllers;

public class HomeController : Controller
{
    private readonly IJokesClient _jokesClient;

    public HomeController(IJokesClient jokesClient)
    {
        _jokesClient = jokesClient;

    }
    public async Task<IActionResult> Index()
    {
        var jokeCount = await _jokesClient.GetCount();

        return jokeCount is not null ? View(jokeCount) : NotFound();
    }

    public async Task<IActionResult> Random()
    {
        var randomJoke = await _jokesClient.GetRandomJoke();

        return randomJoke is not null ? View(randomJoke) : NotFound();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}