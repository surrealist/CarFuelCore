using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFuel.APIs.Models
{
  public class CarResponse
  {
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Color { get; set; }

    public static CarResponse FromModel(Car c)
    {
      return new CarResponse
      {
        Id = c.Id,
        Make = c.Make,
        Color = c.Color
      };
    }
  }
}
