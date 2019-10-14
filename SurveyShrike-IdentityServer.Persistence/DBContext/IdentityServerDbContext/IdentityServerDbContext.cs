/// <summary>
/// @author $username$
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.DBContext.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SurveyShrike_IdentityServer.Application.Interfaces;
    using SurveyShrike_IdentityServer.Domain.Entities;

    /// <summary>
    /// 
    /// </summary>
    public class IdentityServerDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IIdentityServerDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options)
           : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityServerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

           
        }
    }
}
