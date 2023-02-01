using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CeoHelper.Data.Data
{
    public class DbInitializer
    {
        public static void GenerateBaseData(IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ApplicationDbContext context = serviceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
