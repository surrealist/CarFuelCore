using System;
using System.Collections.Generic;
using System.Text;

namespace CarFuel.Models
{
  public class User
  {
    public Guid Id { get; set; }

    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
  }
}
