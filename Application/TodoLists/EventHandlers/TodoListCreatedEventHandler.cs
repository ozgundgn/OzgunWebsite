using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.TodoLists.EventHandlers
{
    internal class TodoListCreatedEventHandler : INotificationHandler<TodoListCreatedEvent>
    {
        private readonly ILogger<TodoListCreatedEventHandler> _logger;

        public TodoListCreatedEventHandler(ILogger<TodoListCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TodoListCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("OzgunReact Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
