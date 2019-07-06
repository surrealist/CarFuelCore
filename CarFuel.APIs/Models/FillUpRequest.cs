using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFuel.APIs.Models
{
  public class FillUpRequest
  {
    public int Odometer { get; set; }
    public double Liters { get; set; }
  }

  public class FillUpResponse
  {
    public int Id { get; set; }
    public int Odometer { get; set; }
    public double Liters { get; set; }

    public static FillUpResponse FromModel(FillUp item)
    {
      return new FillUpResponse
      {
        Id = item.Id,
        Odometer = item.Odometer,
        Liters = item.Liters
      };
    }
  }
}
