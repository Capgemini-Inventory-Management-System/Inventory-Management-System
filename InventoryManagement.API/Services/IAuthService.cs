namespace InventoryManagement.API.Services
{
    public interface IAuthService
    {
        string Register(User user);
        string Login(LoginModel model);
    }
}
