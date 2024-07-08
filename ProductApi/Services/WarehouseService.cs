using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class WarehouseService
    {
        private readonly ApplicationDbContext _context;

        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync() => await _context.Warehouses.ToListAsync();

        public async Task<Warehouse> GetByIdAsync(int id) => await _context.Warehouses.FindAsync(id);

        public async Task AddAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Entry(warehouse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return false;

            var inventory = await _context.Avaibilities.FirstOrDefaultAsync(i => i.WarehouseId == id);
            if (inventory != null) return false; // Warehouse has inventory, cannot delete

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
