using Shipments.Models;
using Shipments.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Shipments.Controllers
{
    [ApiController]
    [Route("/api/shipments")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _srv;

        public ShipmentController(IShipmentService srv)
        {
            _srv = srv;
        }

        [HttpGet]
        public async Task<List<Shipment>> GetAll()
        {
            return await _srv.GetAll();
        }

        [HttpGet("merchant/{merchantId}")]
        public async Task<List<MerchantShipment>> GetShipmentsByMerchant(int merchantId)
        {
            return await _srv.GetShipmentsByMerchant(merchantId);
        }

        [HttpGet("shipmentitems/{shipmentId}")]
        public async Task<List<ShipmentItemDetails>> GetShipmentItemsById(int shipmentId)
        {
            return await _srv.GetShipmentItemsById(shipmentId);
        }

        [HttpGet("{shipmentId}")]
        public async Task<Shipment> GetById(int shipmentId)
        {
            return await _srv.GetById(shipmentId);
        }

        [HttpGet("status/{shipmentId}")]
        public async Task<bool> IsShipmentStatusDelivered(int shipmentId)
        {
            return await _srv.IsShipmentStatusDelivered(shipmentId);
        }

        [HttpGet("vehicles/{vehicleId}")]
        public async Task<List<Shipment>> GetShipmentByVehicleId(int vehicleId)
        {
            return await _srv.GetShipmentByVehicleId(vehicleId);
        }

        [HttpPatch("status/{shipmentId}")]
        public async Task<bool> UpdateStatus(int shipmentId, [FromBody] UpdateStatus statusObject)
        {
            return await _srv.UpdateStatus(shipmentId, statusObject);
        }

        [HttpPost]
        public async Task<bool> Insert(Shipment shipment)
        {
            return await _srv.Insert(shipment);
        }

        [HttpPut]
        public async Task<bool> Update(Shipment shipment)
        {
            return await _srv.Update(shipment);
        }

        [HttpDelete("{shipmentId}")]
        public async Task<bool> Delete(int shipmentId)
        {
            return await _srv.Delete(shipmentId);
        }
    }
}
