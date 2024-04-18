using Umbraco.Commerce.Common;
using Umbraco.Commerce.Reviews.Persistence.Repositories;
using Umbraco.Commerce.Reviews.Persistence.Repositories.Implement;
using Umbraco.Commerce.Infrastructure;
using Umbraco.Cms.Core.Scoping;

namespace Vendr.Contrib.Reviews.Persistence
{
    public class ReviewRepositoryFactory : IReviewRepositoryFactory
    {
        private readonly IScopeAccessor _scopeAccessor;

        public ReviewRepositoryFactory(IScopeAccessor scopeAccessor)
        {
            _scopeAccessor = scopeAccessor;
        }

        public IReviewRepository CreateReviewRepository(IUnitOfWork uow)
        {
            return new ReviewRepository((IDatabaseUnitOfWork)uow, _scopeAccessor.AmbientScope.SqlContext);
        }
    }
}