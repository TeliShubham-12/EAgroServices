using System.Reflection.Metadata;
using System.Threading.Tasks;
using MerchantsAPI.Models;
using MerchantsAPI.Services;
using MerchantsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace MerchantsAPI.Controller;
[ApiController]
[Route("/api/[controller]")]
public class MerchantsController : ControllerBase
{
    private readonly IMerchantService _service;
    public MerchantsController(IMerchantService service)
    {
        this._service = service;
    }
    [HttpGet]
    public async Task<List<Merchant>> GetMerchants()
    {
        return await _service.GetMerchants();
    }
    [HttpGet("{id}")]
    public async Task<Merchant> GetMerchant(int id)
    {
        return await _service.GetMerchant(id);
    }
      [HttpGet("{id}/sellsrecord")]
    public async Task<List<MerchantRecord>> GetMerchantSellRecords(int id)
    {
        return await _service.GetMerchantSellRecords(id);
    }

   [HttpGet("{id}/sellsrecordbydate")]
    public async Task<List<MerchantRecord>> GetMerchantSellRecordsByDate(int id,[FromBody] DateFilter dateFilter)
    {
        return await _service.GetMerchantSellRecordsByDate(id,dateFilter);
    }
    [HttpGet("search")]
    public async Task<List<Merchant>> SearchByName(  [FromQuery] string name){
        System.Console.WriteLine("Hi");
        System.Console.WriteLine(name);
        return await _service.SearchByName(name);
    }
    
}
