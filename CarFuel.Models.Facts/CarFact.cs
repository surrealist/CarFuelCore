using System;
using Xunit;

namespace CarFuel.Models.Facts
{
  public class CarFact
  {
    public class GeneralUsage
    {
      [Fact]
      public void NewCar()
      {
        var c = new Car();

        Assert.NotNull(c.FillUps);
        Assert.Empty(c.FillUps);
      }
    }

    public class AddFillUp : IDisposable
    {
      private Car c;

      public AddFillUp()
      {
        c = new Car();
      }

      [Fact]
      public void AddSingleFillUp()
      {
        //var c = new Car();

        FillUp f = c.AddFillUp(odometer: 1000, 
          liters: 50.0);

        Assert.Equal(1, c.FillUps.Count);
        Assert.NotNull(f);
        Assert.Equal(1000, f.Odometer);
        Assert.Equal(50.0, f.Liters);
        Assert.Null(f.NextFillUp);
      }

      [Fact]
      public void AddTwoFillUps()
      {
        //var c = new Car();

        FillUp f1 = c.AddFillUp(odometer: 1000,
          liters: 60.0);

        FillUp f2 = c.AddFillUp(odometer: 1600,
          liters: 50.0);

        Assert.Equal(2, c.FillUps.Count);
        Assert.Same(f2, f1.NextFillUp);
        Assert.Equal(12.0, f1.KmL);
      }

      public void Dispose()
      {
        c = null;
      }
    }

    public class AverageKmL
    {

    }

  }
}
