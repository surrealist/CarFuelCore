using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFuel.APIs.Models;
using CarFuel.Models;
using CarFuel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CarFuel.APIs.Controllers
{
  [Authorize]
  [Route("api/v1/[controller]")]
  [ApiController]
  [Produces("application/json")]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class CarsController : AppController
  {

    public CarsController(App app, IHttpContextAccessor ctx)
      : base(app, ctx)
    {
      //
    }


    /// <summary>
    /// Get all of your cars.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<CarResponse>> GetAll()
    {
      var cars = app.Cars.All.Select(x => CarResponse.FromModel(x)).ToList();
      return cars;
    }

    /// <summary>
    /// Get a car
    /// </summary>
    /// <param name="id">Car Id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<CarResponse> GetById(Guid id)
    {
      var car = app.Cars.Find(id);
      if (car == null) return NotFound(new ProblemDetails { Title = "Invalid car id" });

      return CarResponse.FromModel(car);
    }

    [HttpPost]
    public ActionResult<Car> Post(CarsPostRequest item)
    {
      var c = new Car();
      c.Make = item.Make;
      c.Color = item.Color;

      try
      {
        app.Cars.Add(c);
        app.SaveChanges();
      }
      catch (Exception ex)
      {
        return BadRequest(new ProblemDetails { Title = ex.Message });
      }

      return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
    }

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, Car item)
    {
      if (id != item.Id)
      {
        return BadRequest();
      }

      app.Cars.Update(item);
      app.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
      var item = app.Cars.Find(id);
      if (item == null) return NotFound();

      app.Cars.Remove(item);
      app.SaveChanges();

      return Ok(item);
    }

    //
    [HttpPost("{id}/FillUps")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
    public ActionResult<FillUpResponse> AddFillUp(Guid id, FillUpRequest item)
    {
      var c = app.Cars.Find(id);
      if (c == null) return NotFound();

      app.Cars.LoadFillUps(c);

      var f = c.AddFillUp(item.Odometer, item.Liters);

      app.SaveChanges();

      return CreatedAtAction(
        nameof(GetFillUpById), new { id = id, fillUpId = f.Id },
        FillUpResponse.FromModel(f));
    }

    [HttpGet("{id}/FillUps")]
    //[ProducesDefaultResponseType(typeof())]
    public ActionResult<IEnumerable<FillUpResponse>> GetAllFillUps(Guid id)
    {
      var c = app.Cars.Find(id);
      if (c == null) return NotFound();
      app.Cars.LoadFillUps(c);

      var items = c.FillUps.Select(x => FillUpResponse.FromModel(x)).ToList();
      return items;
    }

    [HttpGet("{id}/FillUps/{fillUpId}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]
    public ActionResult<FillUpResponse> GetFillUpById(Guid id, int fillUpId)
    {
      var c = app.Cars.Find(id);
      if (c == null) return NotFound();
      app.Cars.LoadFillUps(c);
      var items = c.FillUps.Select(x => FillUpResponse.FromModel(x)).ToList();

      var item = items.SingleOrDefault(x => x.Id == fillUpId);
      return item;
    }
  }
}