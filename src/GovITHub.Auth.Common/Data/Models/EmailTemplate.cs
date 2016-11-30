using System.ComponentModel.DataAnnotations;

namespace GovITHub.Auth.Common.Data.Models
{
    public class EmailTemplate
    {
        public long Id { get; set; }
        public long OrganizationSettingId { get; set; }
        public OrganizationSetting OrganizationSetting { get; set; }
        [StringLength(20)]
        [MaxLength(20)]
        [Required]
        public string Key { get; set; }
        [MaxLength(8196)]
        public string Value { get; set; }
    }
}
