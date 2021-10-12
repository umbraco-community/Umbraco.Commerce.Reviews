using Vendr.Contrib.Reviews.Composing;

#if NETFRAMEWORK
using Umbraco.Core;
using Umbraco.Core.Composing;
#else
using Umbraco.Cms;
using Umbraco.Cms.Core.Composing;
#endif

namespace Vendr.Contrib.Reviews.Composing
{
    // TODO: Update to work with .NET Framework / .NET5
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MigrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<MigrationComponent>();
        }
    }
}