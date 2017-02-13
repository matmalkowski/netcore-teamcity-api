using Autofac;
using System.Linq;
using System.Reflection;

namespace NetCoreTeamCity
{
    internal class BootStrapper
    {
        private readonly object[] _overrides;

        public BootStrapper(params object[] overrides)
        {
            _overrides = overrides;
        }

        public T Get<T>()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(BootStrapper).GetTypeInfo().Assembly).AsImplementedInterfaces();
            RegisterOverrides(builder, _overrides);

            var container = builder.Build();
            return container.Resolve<T>();
        }

        private static void RegisterOverrides(ContainerBuilder builder, params object[] overrides)
        {
            if (overrides == null || !overrides.Any()) return;
            foreach (var instance in overrides)
            {
                builder.RegisterInstance(instance).AsImplementedInterfaces();
            }
        }
    }
}
