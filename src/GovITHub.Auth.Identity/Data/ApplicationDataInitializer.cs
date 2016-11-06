using Microsoft.EntityFrameworkCore;

namespace GovITHub.Auth.Identity.Data
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
        }
    }
}
