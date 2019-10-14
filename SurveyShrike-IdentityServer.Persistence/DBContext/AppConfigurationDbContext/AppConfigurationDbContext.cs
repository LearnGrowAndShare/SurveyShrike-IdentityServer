using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:44:55 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.AppConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfigurationDbContext : ConfigurationDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="storeOptions"></param>
        public AppConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options,
            ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppConfigurationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
