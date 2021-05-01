using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace CalendarApp.Domain
{
    public class BaseEntity<T>
    {
        public BaseEntity()
        {
            // CreatedDate = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public Guid TenantId { get; set; }
        // public DateTime CreatedDate { get; set; }
    }
}