using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SavingStateDemo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var channelId = message.ChannelId;
            var conversationId = message.Conversation.Id;
            var userId = message.From.Id;

            var currentTime = DateTime.Now;

            DateTime firstLoginTimeThisConversationAnyUser = DateTime.MinValue;

            context.ConversationData.TryGetValue<DateTime>("FirstLoginTime", out firstLoginTimeThisConversationAnyUser);
            if (firstLoginTimeThisConversationAnyUser == System.DateTime.MinValue)
            {
                firstLoginTimeThisConversationAnyUser = System.DateTime.Now;
                context.ConversationData.SetValue<DateTime>("FirstLoginTime", firstLoginTimeThisConversationAnyUser);
            }

            DateTime firstLoginTimeEverForCurrentUser = DateTime.MinValue;
            context.UserData.TryGetValue<DateTime>("FirstLoginTime", out firstLoginTimeEverForCurrentUser);
            if (firstLoginTimeEverForCurrentUser == System.DateTime.MinValue)
            {
                firstLoginTimeEverForCurrentUser = currentTime;
                context.UserData.SetValue<DateTime>("FirstLoginTime", firstLoginTimeEverForCurrentUser);
            }

            DateTime firstLoginTimeCurrentConversationCurrentUser = DateTime.MinValue;
            context.PrivateConversationData.TryGetValue<DateTime>("FirstLoginTime", out firstLoginTimeCurrentConversationCurrentUser);
            if (firstLoginTimeCurrentConversationCurrentUser == System.DateTime.MinValue)
            {
                firstLoginTimeCurrentConversationCurrentUser = currentTime;
                context.PrivateConversationData.SetValue<DateTime>("FirstLoginTime", firstLoginTimeCurrentConversationCurrentUser);
            }

            var output = $"User: {userId}\nConversation: {conversationId}\nChannel: {channelId}\n\n";
            output += $"A user logged into this converstaion at {firstLoginTimeThisConversationAnyUser}.\n";
            output += $"You first logged into any conversation at {firstLoginTimeEverForCurrentUser}.\n";
            output += $"You first logged into this conversation at {firstLoginTimeCurrentConversationCurrentUser}.";
            await context.PostAsync(output);

            context.Wait(MessageReceivedAsync);
        }
    }
}