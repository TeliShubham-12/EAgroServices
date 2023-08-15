using BIService.Services.Interfaces;
using BIService.Repositories.Interfaces;
using BIService.Models;

namespace BIService.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _repo;

        public FarmerService(IFarmerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<YearRevenue>> GetRevenuesByYear(int farmerId)
        {
            return await _repo.GetRevenuesByYear(farmerId);
        }
        
        }
        }