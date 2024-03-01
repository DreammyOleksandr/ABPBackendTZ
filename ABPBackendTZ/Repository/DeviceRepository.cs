using ABPBackendTZ.Models;
using ABPBackendTZ.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository;

public class DeviceRepository : IDeviceRepository
{
    private readonly DbContext _context;

    public DeviceRepository(DbContext context)
    {
        _context = context;
    }

    public Device GetByToken(string token) => _context.Set<Device>().Find(token);
    public IEnumerable<Device> GetAll() => _context.Set<Device>().ToList();
    
    public void Add(Device device)
    {
        _context.Set<Device>().Add(device);
        _context.SaveChanges();
    }

    public void Update(Device device)
    {
        _context.Entry(device).State = EntityState.Modified;
        _context.SaveChanges();
    }    
}