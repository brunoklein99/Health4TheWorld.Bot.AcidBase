using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Health4TheWorld.Bot.AcidBase.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ProcessHelloMessage);
        }

        private async Task ProcessHelloMessage(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Call(new CheckPhDialog(), CheckPaCoHcoAsync);
        }

        private async Task CheckPaCoHcoAsync(IDialogContext context, IAwaitable<bool> awaitable)
        {
            var result = await awaitable;

            if (result)
            {
                context.Call(new CheckPaCoHcoDialog(), CheckAnionGapAsync);
            }
            else
            {
                //
            }
        }

        private Task CheckAnionGapAsync(IDialogContext context, IAwaitable<bool> result)
        {
            throw new NotImplementedException();
        }
    }
}