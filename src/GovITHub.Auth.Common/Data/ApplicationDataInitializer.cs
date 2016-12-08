using Microsoft.EntityFrameworkCore;

namespace GovITHub.Auth.Common.Data
{
    public class ApplicationDataInitializer
    {
        private ApplicationDbContext context;

        public ApplicationDataInitializer(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public void InitializeData()
        {
            context.Database.Migrate();
        }
    }
}
