using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewDialogDemo.Dialogs
{
    [Serializable]
    public class TimeNowDialog : IDialog
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // Return our reply to the user
            await context.PostAsync($"The time is now {System.DateTime.Now}");

            context.Wait(MessageReceivedAsync);
        }
    }
}