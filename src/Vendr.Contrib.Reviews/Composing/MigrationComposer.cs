using Vendr.Contrib.Reviews.Composing;

#if NETFRAMEWORK
using Umbraco.Core;
using Umbraco.Core.Composing;
#else
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
#endif

namespace Vendr.Contrib.Reviews.Composing
{
    // TODO: Update to work with .NET Framework / .NET5

#if NETFRAMEWORK
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MigrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<MigrationComponent>();
        }
    }
#else
    public class MigrationComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Components().Append<MigrationComponent>();
        }
    }
#endif
}