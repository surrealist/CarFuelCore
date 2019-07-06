using CarFuel.Models;
using CarFuel.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarFuel.Services
{
  public class App
  {
    internal readonly AppDb db;

    private Lazy<CarService> _carService; 

    public App(AppDb db)
    {
      this.db = db;

      //Cars = new CarService(this);
      _carService = new Lazy<CarService>(() => new CarService(this));

      Users = new UserService(this);
    }

    //public Guid CurrentUserId { get; set; }
    //public string CurrentUserName { get; set; }
    public bool IsAuthenticated { get; set; }
    public User CurrentUser { get; set; } = null;

    public CarService Cars => _carService.Value;
    public UserService Users { get; }


    public int SaveChanges()
    {
      return db.SaveChanges();
    }
  }
}
