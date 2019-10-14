using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityServerInitializer
    {
        public static void Initialize(IdentityServerDbContext context)
        {
            var initializer = new IdentityServerInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(IdentityServerDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; 
            }
        }

    }
}
