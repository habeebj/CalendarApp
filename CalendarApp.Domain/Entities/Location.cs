using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Domain.Entities
{
    public class Location : BaseEntity<string>
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        [Required]
        public string ManagerId { get; private set; }
        public ApplicationUser Manager { get; private set; }

        public static Location Create(ApplicationUser manager, string name, string address)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (address == null)
                throw new ArgumentNullException(nameof(address));

            return new Location
            {
                Manager = manager,
                ManagerId = manager.Id,
                Name = name,
                Address = address,
                TenantId = manager.CompanyId
            };
        }
    }
}