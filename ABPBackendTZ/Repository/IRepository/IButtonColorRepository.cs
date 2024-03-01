using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IButtonColorRepository
{
    Task<ButtonColor> GetById(int? id);
    Task<IEnumerable<ButtonColor>> GetAll();
    Task Add(ButtonColor buttonColor);
    Task Update(ButtonColor buttonColor);
}