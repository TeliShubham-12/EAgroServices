using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.Models;
using UsersAPI.Repositories.Interfaces;
using UsersAPI.Services.Interfaces;
namespace UsersAPI.Services;
public class UserService:IUserService{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo){
        _repo=repo;
    }
    public async Task<List<User>> GetAll()=>await _repo.GetAll();
    public async Task<User> GetUser(int userId)=>await _repo.GetUser(userId);

}