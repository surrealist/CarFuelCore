using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarFuel.Web.Models;
using CarFuel.Services;
using CarFuel.Models;

namespace CarFuel.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly App app;

    public HomeController(App app)
    {
      this.app = app;
    }

    public IActionResult Index()
    {
      //var c = new Car();

      //FillUp f1 = c.AddFillUp(1000, 50.0); // 10.0
      //FillUp f2 = c.AddFillUp(1600, 60.0); // 12.0
      //FillUp f2 = c.AddFillUp(2200, 50.0);

      //app.Cars.Add(c);

      //app.SaveChanges();

      return View();

      //return Content(c.AverageKmL); // 10.91 km/liters
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
}
