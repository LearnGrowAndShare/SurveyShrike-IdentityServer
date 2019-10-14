using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using SurveyShrike_IdentityServer.Persistence.Infrastructure;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:48:53 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.AppConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfigurationDbContextFactory : DesignTimeDbContextFactoryBase<AppConfigurationDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected override AppConfigurationDbContext CreateNewInstance(DbContextOptions<AppConfigurationDbContext> options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            var extension = options.FindExtension<SqliteOptionsExtension>();
            optionsBuilder.UseSqlite(extension.ConnectionString);

            return new AppConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}
