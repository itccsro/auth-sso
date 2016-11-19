using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GovITHub.Auth.Identity.Data.Models
{
    public class Organization
    {
        public long Id { get; set; }
        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public Organization Parent { get; set; }
        public List<Organization> Children { get; set; }
        public List<OrganizationUser> Users { get; set; }
        public OrganizationSetting OrganizationSetting { get; set; }
        public List<OrganizationClient> OrganizationClients { get; set; }
    }
}
