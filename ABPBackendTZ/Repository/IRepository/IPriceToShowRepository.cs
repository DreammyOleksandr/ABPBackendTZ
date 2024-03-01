using ABPBackendTZ.Models;

namespace ABPBackendTZ.Repository.IRepository;

public interface IPriceToShowRepository
{
    PriceToShow GetById(int id);
    IEnumerable<PriceToShow> GetAll();
    void Add(PriceToShow priceToShow);
    void Update(PriceToShow priceToShow);
}