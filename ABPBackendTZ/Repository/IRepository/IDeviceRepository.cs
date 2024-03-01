using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IDeviceRepository
{
    Task<Device> GetByToken(string token, string? includeProperties = null);
    Task<IEnumerable<Device>> GetAll();
    Task Add(Device device);
    Task Update(Device device);
}