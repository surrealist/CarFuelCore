using Microsoft.AspNetCore.Mvc;

namespace CarFuel.APIs.Models
{
  public class UserInfo 
  {
    public string UserName { get; set; }
    public int LuckyNumber { get; internal set; }
  }
}