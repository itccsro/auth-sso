using GovITHub.Auth.Common.Models;

namespace GovITHub.Auth.Common.Data.Models
{
    public class OrganizationUser
    {
        public long Id { get; set; }
        public long? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public OrganizationUserLevel Level { get; set; }
        public short Status { get; set; }
    }
}
