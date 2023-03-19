using FarmersAPI.Models;
namespace FarmersAPI.Repositories.Interfaces;
public interface IFarmerRepository{
    List<Farmer> GetAllFarmers();
    Farmer GetFarmerById(int id);
    bool InsertFarmer(Farmer farmer);
    bool UpdateFarmer(Farmer farmer);
    bool DeleteFarmer(int id);

    
}