using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SniffSandbox;

public class SniffDbContext : IdentityDbContext<IdentityUser>
{
    public SniffDbContext(DbContextOptions<SniffDbContext> options) : base(options)
    {
        
    }
}