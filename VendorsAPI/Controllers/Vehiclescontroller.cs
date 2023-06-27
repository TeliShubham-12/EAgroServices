using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using VendorsAPI.Models;
using VendorsAPI.Services.Interfaces;
namespace VendorsAPI.Controller;
[ApiController]
[Route("/api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleServices _service;
    public VehiclesController(IVehicleServices service)
    {
        this._service = service;
    }
    [HttpGet]
    public async Task<IEnumerable<Vehicle>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Vehicle> GetById(int id)
    {
        return await _service.GetById(id);
    }

    
    [HttpGet("vehicles")]
    public async Task<List<string>> GetVehicles(){
        return await _service.GetVehicles();
    }

    [HttpPost("{id}")]
    public async Task<bool> Insert(int id,[FromBody] Vehicle Vehicle)
    {
       Vehicle.VendorId=id;
    System.Console.WriteLine(Vehicle.Id);
       System.Console.WriteLine(Vehicle.VendorId);
       System.Console.WriteLine(Vehicle.VehicleNumber);
        return await _service.Insert(Vehicle);
    }
    [HttpPut("{id}")]
    public async Task<bool> Update(int id, [FromBody] Vehicle Vehicle)
    {
        return await _service.Update(id, Vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _service.Delete(id);
    }


    [HttpPost("{id}/date")]

   public async Task<List<SellTransport>> GetTransportDetails(int id,[FromBody] StartDateFilter startDate){
    System.Console.WriteLine(startDate.Date);
   return await _service.GetTransportDetails(id,startDate);
}
}