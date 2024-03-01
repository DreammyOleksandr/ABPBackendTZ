using ABPBackendTZ.Models;
using ABPBackendTZ.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ.Repository;

public class ButtonColorRepository : IButtonColorRepository
{
    private readonly DbContext _context;

    public ButtonColorRepository(DbContext context)
    {
        _context = context;
    }

    public ButtonColor GetById(int id) => _context.Set<ButtonColor>().Find(id);
    public IEnumerable<ButtonColor> GetAll() => _context.Set<ButtonColor>().ToList();

    public void Add(ButtonColor buttonColor)
    {
        _context.Set<ButtonColor>().Add(buttonColor);
        _context.SaveChanges();
    }

    public void Update(ButtonColor buttonColor)
    {
        _context.Entry(buttonColor).State = EntityState.Modified;
        _context.SaveChanges();
    }
}