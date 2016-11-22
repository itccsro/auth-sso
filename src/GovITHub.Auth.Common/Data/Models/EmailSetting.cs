using System.ComponentModel.DataAnnotations;

namespace GovITHub.Auth.Common.Data.Models
{
    public class EmailSetting
    {
        public long Id { get; set; }
        public long EmailProviderId { get; set; }
        public EmailProvider EmailProvider { get; set; }
        [MaxLength(8196)]
        public string Settings { get; set; }
        public OrganizationSetting OrganizationSetting { get; set; }
    }
}