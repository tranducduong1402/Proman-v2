using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProMan.Authorization.Roles;
using ProMan.Authorization.Users;
using ProMan.MultiTenancy;
using ProMan.Entities;

namespace ProMan.EntityFrameworkCore
{
    public class ProManDbContext : AbpZeroDbContext<Tenant, Role, User, ProManDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Ticketes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<CommentTicket> CommentTicketes { get; set; }
        public DbSet<ColumnStatus> ColumnStatuses { get; set; }
        public ProManDbContext(DbContextOptions<ProManDbContext> options)
            : base(options)
        {
        }
    }
}
