using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Receive
{
    public class ExampleConsumer : IConsumer<IExampleMessage>
    {
        public async Task Consume(ConsumeContext<IExampleMessage> context)
        {
            // do something...
            await Console.Out.WriteLineAsync($"Updating customer: {context.Message.StringData}");
            await Task.CompletedTask.ConfigureAwait(false);
        }

    }
}