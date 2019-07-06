using System;
using System.Collections.Generic;

namespace CarFuel.Models
{
  public class FillUp
  {

    public int Id { get; set; }

    public FillUp(int odometer, double liters)
    {
      Odometer = odometer;
      Liters = liters;
      IsFull = true;
    }

    public int Odometer { get; set; }
    public double Liters { get; set; }
    public bool IsFull { get; set; }

    public FillUp NextFillUp { get; set; }

    public double? KmL
    {
      get
      {
        if (NextFillUp == null) return null;
        if (NextFillUp.Odometer < Odometer)
        {
          var ex = new InvalidOperationException("Error! Invalid odometer.");
          throw ex;
        }

        var kml = (NextFillUp.Odometer - this.Odometer)
           / NextFillUp.Liters;

        return Math.Round(kml, 2, MidpointRounding.AwayFromZero);
      }
    }

    public override string ToString()
    {
      return $"FillUp: {Odometer,6} {Liters,6:n2} {KmL,6:n2}";
    }

  }
}