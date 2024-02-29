using Microsoft.EntityFrameworkCore;

namespace ABPBackendTZ;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
            
    }
}