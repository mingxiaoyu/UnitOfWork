using Microsoft.EntityFrameworkCore;
using Mingxiaoyu.Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ServiceCollection { get; set; }

        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext, IDbContext
            => AddUnitOfWork<TContext>(services, optionsAction, AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null, params Assembly[] assemblies)
            where TContext : DbContext, IDbContext
        {
            if (optionsAction != null)
                services.AddDbContext<TContext>(optionsAction);

            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            assemblies = assemblies.Where(x => x != typeof(IRepository<>).Assembly).ToArray();

            services.AddScoped<IDbContext, TContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddSingleton<IAssembliesProvider, AssembliesProvider>(facory => { return new AssembliesProvider(assemblies); });

            AddRepository(services, typeof(IRepository<>), assemblies);
            AddRepository(services, typeof(IReadOnlyRepository<>), assemblies);
            ServiceCollection = services;

            return services;
        }

        private static void AddRepository(IServiceCollection services, Type repositoryType, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                                    .Where(x => x.IsClass
                                            && !x.IsAbstract
                                            && x.BaseType != null
                                            && x.HasImplementedRawGeneric(repositoryType));

                foreach (var type in types)
                {
                    var interfaceType = type.GetInterface(repositoryType.Name);
                    if (interfaceType == null) interfaceType = type;
                    var serviceDescriptor = new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped);
                    if (!services.Contains(serviceDescriptor)) services.Add(serviceDescriptor);

                    serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Scoped);
                    if (!services.Contains(serviceDescriptor)) services.Add(serviceDescriptor);
                }
            }
        }

        private static bool HasImplementedRawGeneric(this Type type, Type generic)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (generic == null) throw new ArgumentNullException(nameof(generic));
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }
            return false;

            bool IsTheRawGenericType(Type test)
                => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }
    }
}