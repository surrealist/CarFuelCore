using CarFuel.Models;
using CarFuel.Services.Bases;
using CarFuel.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarFuel.Services
{
  public class UserService : ServiceBase<User>
  {
    public UserService(App app) : base(app)
    {
      //
    }

    public override User Add(User item)
    {
      throw new InvalidOperationException("Please use SignUp to add new user");
    }

    public bool IsValid(string userName, string password)
    {
      var u = Query(x => x.UserName == userName).SingleOrDefault();
      if (u == null) return false;

      var hash = HashPassword(password, u.Id.ToString());
      if (hash != u.PasswordHash) return false;

      return true;
    }

    public User SignUp(string userName, string password)
    {
      //      if (All.Any(x => x.UserName == userName))

      var check = Query(x => x.UserName == userName).SingleOrDefault();
      {
      if (check != null)
        throw new Exception("UserName was already existed");
      }

      var u = new User();

      u.Id = Guid.NewGuid();
      u.UserName = userName;
      u.PasswordHash = HashPassword(text: password, salt: u.Id.ToString());

      base.Add(u);
      app.SaveChanges();

      return u;
    }

    private string HashPassword(string text, string salt)
    {
      text += salt;

      var input = Encoding.Unicode.GetBytes(text);
      using (var hasher = SHA256.Create())
      {
        var output = hasher.ComputeHash(input);

        // Convert byte array to a string   
        StringBuilder builder = new StringBuilder(64);
        for (int i = 0; i < output.Length; i++)
        {
          builder.Append(output[i].ToString("x2"));
        }
        return builder.ToString();
      }
    }
  }
}
