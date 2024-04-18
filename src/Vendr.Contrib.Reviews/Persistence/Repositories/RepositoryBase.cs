using Umbraco.Commerce.Core.Persistence.Repositories;

namespace Umbraco.Commerce.Reviews.Persistence.Repositories
{
    internal abstract class RepositoryBase : IRepository
    {
        public virtual void Dispose()
        {
            // Dispose of any resources
        }
    }
}