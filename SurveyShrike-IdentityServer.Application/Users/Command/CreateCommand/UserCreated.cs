using MediatR;
using SurveyShrike_IdentityServer.Application.Interfaces;
using SurveyShrike_IdentityServer.Application.Notification.Modals;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:25:50 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Users.Command.CreateCommand
{
    public class UserCreated : INotification
    {
        public string Resource { get; set; }

        public class ResourceCreatedHandler : INotificationHandler<UserCreated>
        {
            private readonly INotificationService _notification;

            public ResourceCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }
        }
    }
}
