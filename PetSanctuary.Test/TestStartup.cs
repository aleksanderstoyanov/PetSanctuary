namespace PetSanctuary.Test
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using PetSanctuary.Web;
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }
    }
}
