using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IDeviceRepository
{
    Task<Device> GetByToken(string token, bool includeButtonColor, bool includePriceToShow);
    Task<IEnumerable<Device>> GetAll();
    Task Add(Device device);
    Task Update(Device device);
}