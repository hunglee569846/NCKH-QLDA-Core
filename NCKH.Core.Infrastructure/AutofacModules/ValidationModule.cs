using System.Reflection;
using Module = Autofac.Module;
using Autofac;

namespace NCKH.Core.Infrastructure.AutofacModules
{
    public class ValidationModule : Module
    {
        public ValidationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CategoryMetaValidator>()
            //  .As<IValidator<CategoryMeta>>()
            //  .InstancePerLifetimeScope();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Validator"))
                .AsImplementedInterfaces();
        }

    }
}
