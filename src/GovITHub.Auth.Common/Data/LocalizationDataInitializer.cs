using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.EntityFrameworkCore;

namespace GovITHub.Auth.Common.Data
{
    public class LocalizationDataInitializer
    {
        private LocalizationModelContext context;

        public LocalizationDataInitializer(LocalizationModelContext ctx)
        {
            context = ctx;
        }

        public void InitializeData()
        {
            context.Database.Migrate();
        }
    }
}
