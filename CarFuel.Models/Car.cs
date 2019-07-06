using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarFuel.Models
{
  public class Car
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Make { get; set; }
    public string Color { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }

    [Required]
    public User Owner { get; set; }

    public ICollection<FillUp> FillUps { get; set; }
     = new HashSet<FillUp>();



    public bool IsStarted { get; private set; }

    public void Start()
    {
      IsStarted = true;
    }

    public FillUp AddFillUp(int odometer, double liters)
    {
      var f = new FillUp(odometer, liters);

      var last = FillUps.LastOrDefault();
      if (last != null)
      {
        last.NextFillUp = f;
      }

      FillUps.Add(f);

      return f;
    }
  }
}
