using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProMan.Authorization.Roles;
using ProMan.Authorization.Users;
using ProMan.MultiTenancy;

namespace ProMan.EntityFrameworkCore
{
    public class ProManDbContext : AbpZeroDbContext<Tenant, Role, User, ProManDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ProManDbContext(DbContextOptions<ProManDbContext> options)
            : base(options)
        {
        }
    }
}
