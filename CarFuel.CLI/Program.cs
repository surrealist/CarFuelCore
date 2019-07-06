using CarFuel.CLI.APIs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarFuel.CLI
{
  class Program
  {
    static async Task Main(string[] args)
    {
      using (var http = new HttpClient())
      {
        var client = new Client("https://localhost:44379", http);

        var cars = await client.GetAllAsync();
        foreach(var c in cars)
        {
          Console.WriteLine($"{c.Make} ({c.Color}) -- {c.Id}");
        }
      }
    }
  }
}
