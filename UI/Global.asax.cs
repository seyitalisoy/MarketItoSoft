using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Business.DependencyResolvers.Autofac;


namespace UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac ContainerBuilder oluştur
            var builder = new ContainerBuilder();

            // **AutofacBusinessModule**'ü burada ekliyoruz.
            builder.RegisterModule(new AutofacBusinessModel());

            // MVC controllerlarını da ekliyoruz
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Container'ı oluştur ve MVC'ye tanıt
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
