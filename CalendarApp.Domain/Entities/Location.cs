using System;
namespace CalendarApp.Domain.Entities
{
    public class Location : BaseEntity<string>
    {
        public ApplicationUser Manager { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

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
                Name = name,
                Address = address
            };
        }
    }
}