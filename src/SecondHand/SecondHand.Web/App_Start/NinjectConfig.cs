using System;
using System.Data.Entity;
using System.Web;
using SecondHand.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using SecondHand.Data;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Data.Repositories.Contracts;
using AutoMapper;
using Ninject.Web.Mvc.FilterBindingSyntax;
using SecondHand.Web.Infrastructure.ActionFilters;
using System.Web.Mvc;
using SecondHand.Web.Infrastructure.Attributes;
using SecondHand.Services.Notifications.Contracts;
using Microsoft.AspNet.SignalR;
using SecondHand.Services.Notifications.Hubs;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Ninject.Extensions.Interception.Infrastructure.Language;
using SecondHand.Services.Notifications;
using SecondHand.Services.Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectConfig), "Stop")]

namespace SecondHand.Web
{

    public static class NinjectConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterSignalR(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterSignalR(IKernel kernel)
        {
            var depResolver = new NinjectSignalRDependencyResolver(kernel);

            GlobalHost.DependencyResolver = depResolver;

            kernel.Bind<IHubContext>()
                .ToMethod(ctx =>
                {
                    return depResolver.Resolve<IConnectionManager>().GetHubContext<NotificationHub>();
                });
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IUsersService))
                    .SelectAllClasses()
                    .BindDefaultInterface()
                    .Configure((cfg, type) =>
                    {
                        cfg.InRequestScope();
                    });

                x.FromAssemblyContaining(typeof(IUsersRepository))
                    .SelectAllClasses()
                    .BindDefaultInterface();

                //x.FromAssemblyContaining(typeof(IChatNotificationsService))
                //    .SelectAllClasses()
                //    .BindDefaultInterface();
            });


            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InRequestScope();

            kernel.Bind<IMapper>().ToMethod(ctx => Mapper.Instance).InSingletonScope();

            kernel.Bind(typeof(IUserStore<ApplicationUser>)).To(typeof(UserStore<ApplicationUser>));
            kernel.Bind<IAuthenticationManager>().ToMethod(c =>
                HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            kernel.BindFilter<SaveChangesFilter>(FilterScope.Controller, 0).WhenActionMethodHas<SaveChangesAttribute>();
            var chatNotificationService = kernel.Bind<IChatNotificationsService>().To<ChatNotificationsService>();
        }

        /// <summary>
        /// SignalR magic
        /// </summary>
        private class NinjectSignalRDependencyResolver : DefaultDependencyResolver
        {
            private readonly IKernel kernel;
            public NinjectSignalRDependencyResolver(IKernel kernel)
            {
                this.kernel = kernel;
            }

            public override object GetService(Type serviceType)
            {
                return kernel.TryGet(serviceType) ?? base.GetService(serviceType);
            }

            public override System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
            {
                return kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
            }

        }
        //
    }
}
