using Transflower.EAgroservice.Models;

namespace Transflower.EAgroservice.Repositories.Interface
{

    public interface IGoodsCollectionRepository{ 
        public Task<int> GetTotalEntriesForFarmer(int id);

        public  Task<int> GetTotalEntriesForFarmerOnSpecificDate(int id, DateTime collectionDate);

    
       public Task<int> GetTotalEntriesBeetweenDates(int id, DateTime startDate, DateTime endDate);
    }
}