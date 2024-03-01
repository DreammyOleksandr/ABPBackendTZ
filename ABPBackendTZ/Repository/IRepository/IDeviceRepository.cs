using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IDeviceRepository
{
    Device GetByToken(string token);
    IEnumerable<Device> GetAll();
    void Add(Device device);
    void Update(Device device);
}