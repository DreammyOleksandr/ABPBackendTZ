using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IButtonColorRepository
{
    ButtonColor GetById(int id);
    IEnumerable<ButtonColor> GetAll();
    void Add(ButtonColor buttonColor);
    void Update(ButtonColor buttonColor);
}