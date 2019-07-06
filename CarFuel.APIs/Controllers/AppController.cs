using CarFuel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarFuel.APIs.Controllers
{
  public class AppController : ControllerBase
  {

    protected readonly App app;

    public AppController(App app, IHttpContextAccessor ctx)
    {
      this.app = app;

      //this.app.CurrentUserName = User.Identity.Name;
      this.app.IsAuthenticated = ctx.HttpContext.User.Identity.IsAuthenticated;

      if (ctx.HttpContext.User.Identity.IsAuthenticated)
      {
        this.app.CurrentUser = app.Users
          .Query(x => x.UserName == ctx.HttpContext.User.Identity.Name)
          .SingleOrDefault();

        //if (this.app.CurrentUser != null)
        //{
        //  //this.app.CurrentUserId = this.app.CurrentUser.Id;
        //}
      }
    }

  }
}
