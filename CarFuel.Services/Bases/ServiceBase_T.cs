using CarFuel.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFuel.Services.Bases
{
  public class ServiceBase<T> : IService<T> where T : class
  {
    protected readonly App app;

    public ServiceBase(App app) 
      => this.app = app;

    public virtual IQueryable<T> Query(Func<T, bool> condition)
      => app.db.Set<T>().Where(condition).AsQueryable();

    public IQueryable<T> All 
      => Query(x => true);

    public virtual T Find(params object[] keys) 
      => app.db.Set<T>().Find(keys);

    public virtual T Add(T item) 
      => app.db.Set<T>().Add(item).Entity;

    public virtual T Remove(T item)
      => app.db.Set<T>().Remove(item).Entity;

    public virtual T Update(T item)
      => app.db.Set<T>().Update(item).Entity;
  }
}
