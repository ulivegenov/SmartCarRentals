namespace SmartCarRentals.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore.Internal;
    using SmartCarRentals.Common;
    using SmartCarRentals.Web.ViewModels.Main.Chat;

    public class ChatHub : Hub
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();

        public async Task Send(string message)
        {
            var myMessage = new Message()
            {
                User = this.Context.User.Identity.IsAuthenticated ?
                       this.Context.User.Identity.Name :
                       "Guest",
                Text = message,
            };

            await this.Clients.All.SendAsync("NewMessage", myMessage);
        }

        public async Task JoinToGroup(string groupName)
        {
            var currentUser = this.Context.User.Identity.IsAuthenticated ?
                              this.Context.User.Identity.Name :
                              "Guest";

            var sorryMessage = new Message()
            {
                User = "Auto Admin",
                Text = $"Sorry {currentUser}! There are no available operators, at the moment.",
            };

            var adminMessage = new Message()
            {
                User = "Auto Admin",
                Text = $"{currentUser} you are online...",
            };

            var userMessage = new Message()
            {
                User = "Auto Admin",
                Text = $"{currentUser} is joined...",
            };

            if (users.Count == 1)
            {
                if (this.Context.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
                    await this.Clients.All.SendAsync("NewMessage", adminMessage);
                }
                else
                {
                    await this.Clients.Caller.SendAsync("NewMessage", sorryMessage);
                    await this.Clients.Caller.SendAsync("Finished");
                }
            }
            else if (users.Count == 2 && users.ContainsValue(GlobalConstants.AdministratorRoleName))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
                await this.Clients.All.SendAsync("NewMessage", userMessage);
            }
            else
            {
                await this.Clients.Caller.SendAsync("NewMessage", sorryMessage);
                await this.Clients.Caller.SendAsync("Finished");
            }
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = this.Context.ConnectionId;
            var role = this.Context.User.IsInRole(GlobalConstants.AdministratorRoleName) ?
                       GlobalConstants.AdministratorRoleName :
                       GlobalConstants.UserRoleName;

            users.Add(connectionId, role);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = this.Context.ConnectionId;

            users.Remove(connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
