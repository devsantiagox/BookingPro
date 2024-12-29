using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using BookingPro.Repositories;
using BookingPro.Services;
using BookingPro.Data; 

public static class SimpleInjectorConfig
{
    public static void RegisterComponents()
    {
        var container = new Container();
        container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

        container.Register<DbContext>(Lifestyle.Scoped);

        container.Register<SalaRepository>(Lifestyle.Scoped);
        container.Register<SalaService>(Lifestyle.Scoped);
        container.Register<ReservaRepository>(Lifestyle.Scoped);
        container.Register<ReservaService>(Lifestyle.Scoped);

        container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());

        container.Verify();

        DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
    }
}
