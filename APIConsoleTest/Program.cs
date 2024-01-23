using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nominatim.Clients;
using System.Text.Json;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var client = new NominatimClient();
var response = await client.SearchAsync(new Nominatim.Models.StructuredQuerySearchModel
{
    Amenity = "restaurant",
    City = "New York",
    Country = "USA"
});

Console.WriteLine(JsonSerializer.Serialize(response));

Console.ReadLine();

