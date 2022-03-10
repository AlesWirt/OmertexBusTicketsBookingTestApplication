using System.Collections.Generic;

namespace DomainModel.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}