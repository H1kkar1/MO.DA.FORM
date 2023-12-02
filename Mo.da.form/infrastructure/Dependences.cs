namespace MO.DA.FORM.infrastructure
{
    public class Dependences
    {
        public IServiceCollection services;
        Dependences() { }
        public ServiceCollection Get_services()
        {
            services = services != null ? services : new ServiceCollection();
            return (ServiceCollection)services;
        }
    }
}
