using InventoryManagement.API.Models;

namespace InventoryManagement.API.Services
{
    public interface IAuthService
    {
        Task<string> Register(LoginModel model);
        Task<string> Login(LoginModel model);
       
    }
}