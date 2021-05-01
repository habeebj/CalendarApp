using System;

namespace CalendarApp.Domain
{
    public class BaseEntity<T>
    {
        public BaseEntity()
        {
            // CreatedDate = DateTime.Now;
        }
        public T Id { get; set; }
        public Guid TenantId { get; set; }
        // public DateTime CreatedDate { get; set; }
    }
}