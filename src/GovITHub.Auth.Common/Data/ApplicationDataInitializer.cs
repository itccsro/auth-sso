using Microsoft.EntityFrameworkCore;

namespace GovITHub.Auth.Common.Data
{
    public class ApplicationDataInitializer
    {
        private ApplicationDbContext context;

        public ApplicationDataInitializer(ApplicationDbContext ctx)
        {
            context = ctx;
            context.Database.Migrate();
        }

        public void InitializeData()
        {
        }
    }
}
