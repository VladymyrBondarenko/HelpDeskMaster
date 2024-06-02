using HelpDeskMaster.Domain.Abstractions;
using MediatR;

namespace HelpDeskMaster.E2ETests.BackgroundJobsTests.ProcessOutboxMessagesJob
{
    internal record ProcessOutboxMessageTestDomainEvent(
        string content) : IDomainEvent
    {
    }

    internal class EquipmentAssignedToUserDomainEventHandler : INotificationHandler<ProcessOutboxMessageTestDomainEvent>
    {
        public Task Handle(ProcessOutboxMessageTestDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}