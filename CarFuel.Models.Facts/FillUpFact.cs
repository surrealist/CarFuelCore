using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CarFuel.Models.Facts
{
  public class FillUpFact
  {

    public class GeneralUsage
    {
      [Fact]
      public void NewFillUp()
      {
        // a
        FillUp f;

        // a
        f = new FillUp(odometer: 0, liters: 20.0);

        // a
        Assert.Equal(0, f.Odometer);
        Assert.Equal(20.0, f.Liters);
        Assert.True(f.IsFull);

      }
    }

    public class GetKmL
    {
      private readonly ITestOutputHelper output;

      public GetKmL(ITestOutputHelper output)
      {
        this.output = output;
      }

      [Fact]
      public void FirstFillUp_ReturnNull()
      {
        var f = new FillUp(1000, 50.0);

        double? kml = f.KmL;

        Assert.Null(kml);
      }

      [Fact]
      public void TwoFillUps()
      {
        var f1 = new FillUp(1000, 50.0);
        var f2 = new FillUp(1600, 60.0);
        f1.NextFillUp = f2;

        double? kml1 = f1.KmL;
        double? kml2 = f2.KmL;

        output.WriteLine(f1.ToString());
        output.WriteLine(f2.ToString());

        Assert.Equal(10.0, kml1);
        Assert.Null(kml2);
      }

      public static IEnumerable<object[]> GetTestData1(string dataSet = "A")
      {
        if (dataSet == "A") {
          yield return new object[] { 1000, 50.0, 1600, 60.0, 10.0 };
          yield return new object[] { 1000, 50.0, 1200, 40.0, 5.0 };
        }
      }

      [Theory]
      //[InlineData(1000, 50.0, 1600, 60.0, 10.0)]
      //[InlineData(1000, 50.0, 1200, 40.0, 5.0)] 
      [MemberData(nameof(GetTestData1), "A")]
      public void TwoFillUpsParameterized(int odo1, double liter1, int odo2, double liter2,
          double kml)
      {
        var f1 = new FillUp(odo1, liter1);
        var f2 = new FillUp(odo2, liter2);
        f1.NextFillUp = f2;

        double? kml1 = f1.KmL;
        double? kml2 = f2.KmL;

        Assert.Equal(kml, kml1);
        Assert.Null(kml2);
      }


      [Fact]
      public void FourFillUps()
      {
        var f1 = new FillUp(1000, 50.0);
        var f2 = new FillUp(1600, 60.0);
        var f3 = new FillUp(2200, 50.0);
        var f4 = new FillUp(2600, 54.98);

        f1.NextFillUp = f2;
        f2.NextFillUp = f3;
        f3.NextFillUp = f4;

        double? kml = f3.KmL;

        Assert.Equal(7.28, kml);
      }

      [Fact]
      public void InvalidOdometers()
      {
        var f1 = new FillUp(150_000, 50.0);
        var f2 = new FillUp(60_000, 60.0);
        f1.NextFillUp = f2;

        var ex = Assert.ThrowsAny<Exception>(() =>
                  {

                    var kml = f1.KmL;

                  });

        Assert.Contains("Invalid odometer", ex.Message);
      }
    }

  }
}
