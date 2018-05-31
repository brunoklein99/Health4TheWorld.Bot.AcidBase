using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Health4TheWorld.Bot.AcidBase.Dialogs
{
    [Serializable]
    public class CheckPhDialog : IDialog<bool>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //start of conversation
            await context.SayAsync("Step 1: (abctest) Evaluate if primary disorder is acidosis or alkalosis.\nIs pH above or below 7.4?");

            var message = context.MakeMessage();
            
            message.Attachments.Add(CreateButtons());

            await context.PostAsync(message);
            
            //wait for user message and process it with callback
            context.Wait(ProcessButtonAsync);
        }

        private static async Task ProcessButtonAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            if (activity == null)
            {
                throw new InvalidOperationException("activity is null");
            }
            
            context.Done(activity.Text == "below");            
        }

        private static Attachment CreateButtons()
        {
            return new HeroCard
            {
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack, value: "below", title: "Below"),
                    new CardAction(ActionTypes.ImBack, value: "above", title: "Above")
                }
            }.ToAttachment();
        }
    }
}