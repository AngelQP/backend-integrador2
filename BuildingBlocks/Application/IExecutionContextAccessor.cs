using System;

namespace Bigstick.BuildingBlocks.Application
{
    public interface IExecutionContextAccessor
    {
        string UserId { get; }

        string UserName { get; }

        string CodigoSap { get; }

        Guid CorrelationId { get; }

        bool IsAvailable { get; }
    }
}
