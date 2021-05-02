using System;

namespace CalendarApp.Applications.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity) : base($"{entity} not found")
        {
        }
    }
}