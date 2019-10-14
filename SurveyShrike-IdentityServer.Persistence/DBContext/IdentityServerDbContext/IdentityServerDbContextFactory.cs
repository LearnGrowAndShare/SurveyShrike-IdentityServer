using Microsoft.EntityFrameworkCore;
using SurveyShrike_IdentityServer.Persistence.Infrastructure;

/// <summary>
/// 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityServerDbContextFactory : DesignTimeDbContextFactoryBase<IdentityServerDbContext>
    {
        protected override IdentityServerDbContext CreateNewInstance(DbContextOptions<IdentityServerDbContext> options)
        {
            return new IdentityServerDbContext(options);
        }
    }
}
