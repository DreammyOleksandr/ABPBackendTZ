using ABPBackendTZ.Models;
using ABPBackendTZ.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository;

public class DeviceRepository : IDeviceRepository
{
    private readonly ApplicationDbContext _context;

    public DeviceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Device> GetByToken(string token, string? includeProperties = null)
    {
        var query = _context.Devices.AsQueryable();
        
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return await query.FirstOrDefaultAsync(_ => _.Token == token);
    }
    public async Task<IEnumerable<Device>> GetAll() => await _context.Set<Device>().ToListAsync();
    
    public async Task Add(Device device)
    {
        _context.Set<Device>().Add(device);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Device device)
    {
        _context.Entry(device).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }    
}