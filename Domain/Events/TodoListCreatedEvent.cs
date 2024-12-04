using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TodoListCreatedEvent : BaseEvent
    {
        public TodoListCreatedEvent(TodoList item)
        {
            Item = item;
        }

        public TodoList Item { get; set; }
    }
}
