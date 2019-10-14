using SurveyShrike_IdentityServer.Application.Notification.Modals;
using System.Threading.Tasks;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:00:07 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
