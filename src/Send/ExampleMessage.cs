using System;

// The namespace and the interface name have to match for Masstransit to work and send messages
namespace Messages
{
    public interface IExampleMessage
    {
        Guid CorrelationId { get; }

        string StringData { get; }

        DateTime DateTimeData { get; }
    }
}
