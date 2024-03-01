using ABPBackendTZ.Models;
using ABPBackendTZ.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository;

public class ButtonColorRepository : IButtonColorRepository
{
    private readonly ApplicationDbContext _context;

    public ButtonColorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ButtonColor> GetById(int? id) => await _context.Set<ButtonColor>().FindAsync(id);
    public async Task<IEnumerable<ButtonColor>> GetAll() => await _context.Set<ButtonColor>().ToListAsync();

    public async Task Add(ButtonColor buttonColor)
    {
        _context.Set<ButtonColor>().Add(buttonColor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ButtonColor buttonColor)
    {
        _context.Entry(buttonColor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}