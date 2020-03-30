using System.Reflection;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    internal interface IAssembliesProvider
    {
        Assembly[] GetAssemblies();
    }

    internal class AssembliesProvider : IAssembliesProvider
    {
        private readonly Assembly[] _assemblies;

        public AssembliesProvider(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public Assembly[] GetAssemblies()
        {
            return _assemblies;
        }
    }
}