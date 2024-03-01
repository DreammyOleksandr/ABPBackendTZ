using ABPBackendTZ.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository.IRepository;

public class PriceToShowRepository : IPriceToShowRepository
{
    private readonly ApplicationDbContext _context;

    public PriceToShowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PriceToShow> GetById(int id) => await _context.Set<PriceToShow>().FindAsync(id);
    public async Task<IEnumerable<PriceToShow>> GetAll() => await _context.Set<PriceToShow>().ToListAsync();
    
    public async Task Add(PriceToShow priceToShow)
    {
        await _context.Set<PriceToShow>().AddAsync(priceToShow);
        await _context.SaveChangesAsync();
    }

    public async Task Update(PriceToShow priceToShow)
    {
        _context.Entry(priceToShow).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}