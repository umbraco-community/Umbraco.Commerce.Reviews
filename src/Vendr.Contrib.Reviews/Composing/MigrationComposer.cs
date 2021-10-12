using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.Reviews.Composing;

namespace Vendr.Contrib.Reviews.Composers
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