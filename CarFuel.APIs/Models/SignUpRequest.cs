using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarFuel.APIs.Models
{
  public class SignUpRequest
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
