using ProductApi.Data;
using ProductApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductApi.Services
{
    public class AvailabilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AvailabilityService> _logger;

        public AvailabilityService(ApplicationDbContext context, ILogger<AvailabilityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Availability>> GetAllAsync()
        {
            try
            {
                return await _context.Availabilities
                    .Include(a => a.Product)
                    .Include(a => a.Warehouse)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all availabilities");
                return Enumerable.Empty<Availability>();
            }
        }

        public async Task<Availability?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Availabilities
                    .Include(a => a.Product)
                    .Include(a => a.Warehouse)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving availability with ID {id}");
                return null;
            }
        }

        public async Task<Availability?> AddAsync(Availability availability)
        {
            if (availability == null)
            {
                _logger.LogWarning("Attempted to add a null availability");
                return null;
            }

            try
            {
                await _context.Availabilities.AddAsync(availability);
                await _context.SaveChangesAsync();
                return availability; // Return the added availability
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding availability");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Availability availability)
        {
            if (availability == null)
            {
                _logger.LogWarning("Attempted to update a null availability");
                return false;
            }

            try
            {
                _context.Availabilities.Update(availability);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating availability");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var availability = await _context.Availabilities.FindAsync(id);
                if (availability == null)
                {
                    _logger.LogWarning($"Availability with ID {id} not found for deletion");
                    return false;
                }

                _context.Availabilities.Remove(availability);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting availability with ID {id}");
                return false;
            }
        }
    }
}
