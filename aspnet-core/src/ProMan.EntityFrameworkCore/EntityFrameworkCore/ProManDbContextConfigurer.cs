using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ProMan.EntityFrameworkCore
{
    public static class ProManDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProManDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ProManDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
