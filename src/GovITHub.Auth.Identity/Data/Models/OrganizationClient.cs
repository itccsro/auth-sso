
namespace GovITHub.Auth.Identity.Data.Models
{
    public class OrganizationClient
    {
        public int ClientId { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
