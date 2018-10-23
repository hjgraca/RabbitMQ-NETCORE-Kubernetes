using System;

namespace Messages
{
    public interface IExampleMessage
    {
        Guid CorrelationId { get; }

        string StringData { get; }

        DateTime DateTimeData { get; }
    }
}