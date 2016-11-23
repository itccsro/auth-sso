using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GovITHub.Auth.Identity.Data.Models
{
    public class EmailProvider
    {
        public long Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public List<EmailSetting> EmailSettings { get; set; }
    }
}
