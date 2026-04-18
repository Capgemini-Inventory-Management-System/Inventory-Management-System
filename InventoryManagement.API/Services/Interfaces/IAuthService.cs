using InventoryManagement.API.Models.DTOs;

namespace InventoryManagement.API.Services.Interfaces
{
    public interface IAuthService
    {
        string Register(RegistrationDto userDto);
        string Login(LoginDto model);
    }
}
