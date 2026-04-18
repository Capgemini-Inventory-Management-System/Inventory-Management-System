using InventoryManagement.API.Models.DTOs;

namespace InventoryManagement.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomer createDto);
        Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomer updateDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
