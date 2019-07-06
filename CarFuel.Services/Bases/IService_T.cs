using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFuel.Services.Bases
{
  public interface IService<T> where T: class
  {
    IQueryable<T> Query(Func<T, bool> condition);
    IQueryable<T> All { get; }
    T Find(params object[] keys);

    T Add(T item);
    T Update(T item);
    T Remove(T item);
  }
}
