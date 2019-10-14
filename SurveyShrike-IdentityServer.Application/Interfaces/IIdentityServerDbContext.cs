using Microsoft.EntityFrameworkCore;
using SurveyShrike_IdentityServer.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// @author Ankit
/// @date - 10/14/2019 10:57:51 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Interfaces
{
    public interface IIdentityServerDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
