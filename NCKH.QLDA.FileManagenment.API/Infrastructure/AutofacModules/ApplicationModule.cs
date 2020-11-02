using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace NCKH.QLDA.FileManagenment.API.Infrastructure.AutofacModules
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

            //builder.RegisterType<UserService>()
            // .As<IUserService>()
            // .InstancePerLifetimeScope();

            //builder.RegisterType<UserRepository>()
            //.As<IUserRepository>()
            //.InstancePerLifetimeScope();

            //#region Resources
            //builder.RegisterType<ResourceService<GhmFileResource>>()
            //    .As<IResourceService<GhmFileResource>>()
            //    .InstancePerLifetimeScope();
            //#endregion

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
