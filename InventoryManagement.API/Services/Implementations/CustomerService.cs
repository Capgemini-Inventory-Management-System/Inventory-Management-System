using InventoryManagement.API.Data;
using InventoryManagement.API.Models.Entities;
using InventoryManagement.API.Models.DTOs;
using InventoryManagement.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Select(static c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    MobileNumber = c.MobileNumber,
                    Email = c.Email
                })
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                MobileNumber = customer.MobileNumber,
                Email = customer.Email
            };
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomer createDto)
        {
            var customer = new Customer
            {
                CustomerName = createDto.CustomerName,
                MobileNumber = createDto.MobileNumber,
                Email = createDto.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                MobileNumber = customer.MobileNumber,
                Email = customer.Email
            };
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomer updateDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.CustomerName = updateDto.CustomerName;
            customer.MobileNumber = updateDto.MobileNumber;
            customer.Email = updateDto.Email;

            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                MobileNumber = customer.MobileNumber,
                Email = customer.Email
            };
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)   // check if customer has placed orders
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null) return false;

            // Business rule: Cannot delete if customer has existing orders
            if (customer.Orders.Any())
                throw new InvalidOperationException("Cannot delete customer because they have associated orders.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
