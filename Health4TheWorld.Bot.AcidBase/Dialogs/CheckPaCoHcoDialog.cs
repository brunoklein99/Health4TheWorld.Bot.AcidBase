using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Health4TheWorld.Bot.AcidBase.Dialogs
{
    [Serializable]
    public class CheckPaCoHcoDialog : IDialog<bool>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //start of conversation
            await context.SayAsync("Step 2: Limit differential of primary disorder to determine if it is respiratory or metabolic acidosis." +
                                   "\nIf HCO3 < 24 mEg/l proceed with Metabolic Acidosis." +
                                   "\nIf PaCO2 > 40 mm Hg proceed with Respiratory Acidosis.");
            
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
                    new CardAction(ActionTypes.ImBack, value: "below", title: "HCO3 < 24 mEg/l"),
                    new CardAction(ActionTypes.ImBack, value: "above", title: "PaCO2 > 40 mm")
                }
            }.ToAttachment();
        }        
    }
}