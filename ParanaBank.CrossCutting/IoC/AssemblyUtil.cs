using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ParanaBank.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("ParanaBank.Api"),
                Assembly.Load("ParanaBank.Application"),
                Assembly.Load("ParanaBank.Domain"),
                Assembly.Load("ParanaBank.Infrastructure"),
                Assembly.Load("ParanaBank.CrossCutting")
            };
        }
    }
}
