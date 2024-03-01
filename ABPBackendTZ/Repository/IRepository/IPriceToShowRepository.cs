using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IPriceToShowRepository
{
    Task<PriceToShow> GetById(int id);
    Task<IEnumerable<PriceToShow>> GetAll();
    Task Add(PriceToShow priceToShow);
    Task Update(PriceToShow priceToShow);
}