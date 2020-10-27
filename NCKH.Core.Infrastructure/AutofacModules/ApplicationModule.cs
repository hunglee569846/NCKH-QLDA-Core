using Autofac;
using NCKH.Core.Domain.Resources;
using NCKH.Infrastruture.Binding.IServices;
using NCKH.Infrastruture.Binding.Services;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace NCKH.Core.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        public string ConnectionString { get; }
        public ApplicationModule(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //builder.RegisterType<HttpClientService>()
            //  .As<IHttpClientService>()
            //  .InstancePerLifetimeScope();

            //builder.RegisterType<UserService>()
            // .As<IUserService>()
            // .InstancePerLifetimeScope();

            //builder.RegisterType<UserRepository>()
            //.As<IUserRepository>()
            //.InstancePerLifetimeScope();

            #region Resources
            //builder.RegisterType<ResourceService<SharedResource>>()
            //    .As<IResourceService<SharedResource>>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<ResourceService<APINCKHResource>>()
            //    .As<IResourceService<APINCKHResource>>()
            //    .InstancePerLifetimeScope();
            #endregion

            #region Repositories            
            builder.RegisterAssemblyTypes(assembly)
               .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces()
              .WithParameter(new TypedParameter(typeof(string), ConnectionString));
            #endregion

            #region Services            
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            #endregion                    
        }
    }
}

