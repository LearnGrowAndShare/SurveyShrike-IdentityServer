
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using SurveyShrike_IdentityServer.Persistence.Infrastructure;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 2:32:20 PM 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.PersistentDbContext
{
    public class PersistentConfigurationDbContextFactory : DesignTimeDbContextFactoryBase<PersistentConfigurationDbContext>
    {
        protected override PersistentConfigurationDbContext CreateNewInstance(DbContextOptions<PersistentConfigurationDbContext> options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            var extension = options.FindExtension<SqliteOptionsExtension>();
            optionsBuilder.UseSqlite(extension.ConnectionString);

            return new PersistentConfigurationDbContext(optionsBuilder.Options, new OperationalStoreOptions());
        }
    }
}
