using System;
using System.Linq;
using CarFuel.Models;
using CarFuel.Services.Bases;
using CarFuel.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CarFuel.Services
{
  public class CarService : ServiceBase<Car>
  {
    public CarService(App app) : base(app)
    { 
    }

    public override Car Add(Car item)
    {
      if (app.CurrentUser == null) 
        throw new Exception();

      if (All.Count() >= 2) throw new Exception("You can have no more car");

      item.Owner = app.CurrentUser;

      return base.Add(item);
    }
    
    public override IQueryable<Car> Query(Func<Car, bool> condition)
    {
      return base.Query(condition)
        .Where(x => x.Owner == app.CurrentUser)
        .Where(x => !x.IsDeleted);
    }

    public override Car Find(params object[] keys)
    {
      var item = base.Find(keys);
      if (item != null && item.IsDeleted) return null;
      if (item.Owner != app.CurrentUser) return null;

      return item;
    }

    public void LoadFillUps(Car item)
    {
      app.db.Entry(item).Collection(x => x.FillUps).Load();
      //db.Entry(item).Reference(x => x.Something).Load();

    }

    // Soft-Deleting
    public override Car Remove(Car item)
    {
      if (item.IsDeleted) return item;

      item.IsDeleted = true;
      item.DeletedDate = DateTime.Now;

      return item;
    }
  }
}
