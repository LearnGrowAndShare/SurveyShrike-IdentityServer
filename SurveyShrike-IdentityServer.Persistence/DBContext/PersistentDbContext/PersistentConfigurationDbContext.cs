using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 2:31:57 PM 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.PersistentDbContext
{
    public class PersistentConfigurationDbContext : PersistedGrantDbContext
    {
        public PersistentConfigurationDbContext(DbContextOptions<PersistedGrantDbContext> options,
            OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
            storeOptions.EnableTokenCleanup = true;
            storeOptions.TokenCleanupInterval = 60;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistentConfigurationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
