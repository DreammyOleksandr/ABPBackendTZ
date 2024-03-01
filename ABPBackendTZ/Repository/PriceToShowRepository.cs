using ABPBackendTZ.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository.IRepository;

public class PriceToShowRepository : IPriceToShowRepository
{
    private readonly DbContext _context;

    public PriceToShowRepository(DbContext context)
    {
        _context = context;
    }

    public PriceToShow GetById(int id) => _context.Set<PriceToShow>().Find(id);
    public IEnumerable<PriceToShow> GetAll() => _context.Set<PriceToShow>().ToList();
    
    public void Add(PriceToShow priceToShow)
    {
        _context.Set<PriceToShow>().Add(priceToShow);
        _context.SaveChanges();
    }

    public void Update(PriceToShow priceToShow)
    {
        _context.Entry(priceToShow).State = EntityState.Modified;
        _context.SaveChanges();
    }
}