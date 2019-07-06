using CarFuel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarFuel.Services.Data
{
  public class AppDb : DbContext
  {
    public AppDb(DbContextOptions<AppDb> options): base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
  }
}
