using InventaryManagement.Models.DTOs;

namespace InventoryManagement.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(string id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomer createDto);
        Task<CustomerDto?> UpdateCustomerAsync(string id, UpdateCustomer updateDto);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
