using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFuel.Web.ViewComponents
{
  public class SignInViewComponent : ViewComponent
  {

    public IViewComponentResult Invoke()
    {
      return View();
    }
  }
}
