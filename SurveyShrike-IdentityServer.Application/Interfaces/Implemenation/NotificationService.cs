using SurveyShrike_IdentityServer.Application.Notification.Modals;
using System.Threading.Tasks;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:01:34 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Interfaces.Implemenation
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
