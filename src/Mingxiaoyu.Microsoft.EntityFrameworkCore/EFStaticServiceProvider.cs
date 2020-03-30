using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// A lazy loaded thread-safe singleton App ServiceProvider.
    /// It's required for static `Cacheable()` methods.
    /// </summary>
    internal static class EFStaticServiceProvider
    {
        private static readonly Lazy<IServiceProvider> _serviceProviderBuilder =
            new Lazy<IServiceProvider>(getServiceProvider, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Defines a mechanism for retrieving a service object.
        /// </summary>
        public static IServiceProvider Instance { get; } = _serviceProviderBuilder.Value;

        private static IServiceProvider getServiceProvider()
        {
            var serviceProvider = ServiceCollectionExtensions.ServiceCollection?.BuildServiceProvider();
            return serviceProvider ?? throw new InvalidOperationException("Please add `AddUnitOfWork()` method to your `IServiceCollection`.");
        }
    }
}