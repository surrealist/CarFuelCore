using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using CarFuel.APIs.Models;
using Microsoft.Extensions.Configuration;
using CarFuel.Services;

namespace CarFuel.APIs.Controllers
{

  [Authorize(Policy = "HasLuckyNumber")]
  [Route("api/v1/[controller]")]
  [ApiController]
  [Produces("application/json")]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class AuthController : ControllerBase
  {
    private readonly App app;
    private readonly IConfiguration config;

    public AuthController(App app, IConfiguration config)
    {
      this.app = app;
      this.config = config;
    }

    [HttpGet("user")]
    public ActionResult<UserInfo> GetUserInfo()
    {
      var luckyNumber = 0;

      if (User.HasClaim(x => x.Type == "luckyNumber"))
      {
        luckyNumber = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "luckyNumber").Value);
      }

      return new UserInfo
      {
        UserName = User.Identity.Name,
        LuckyNumber = luckyNumber
      };
    }


    [AllowAnonymous]
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public ActionResult<UserInfo> SignUp(SignUpRequest item)
    {
      try
      {
        var u = app.Users.SignUp(item.UserName, item.Password);

        return new UserInfo { UserName = u.UserName };
      }
      catch (Exception ex)
      {
        return BadRequest(new ProblemDetails { Title = ex.Message });
      }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult RequestToken([FromBody] TokenRequest request)
    {
      //if (request.Username == "Jon" && request.Password == "999")
      if (app.Users.IsValid(request.Username, request.Password))
      {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim("luckyNumber", "999")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["SecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        double expireMinutes;
        if (!double.TryParse(config["JWT:ExpireMinutes"], out expireMinutes))
        {
          expireMinutes = 20;
        }


        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            audience: config["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expireMinutes),
            signingCredentials: creds);

        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token)
        });
      }

      return BadRequest("Could not verify username and password");
    }
  }
}