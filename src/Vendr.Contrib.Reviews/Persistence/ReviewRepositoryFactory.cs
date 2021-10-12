using Vendr.Common;
using Vendr.Contrib.Reviews.Persistence.Repositories;
using Vendr.Contrib.Reviews.Persistence.Repositories.Implement;
using Vendr.Infrastructure;

#if NETFRAMEWORK
using Umbraco.Core.Scoping;
#else
using Umbraco.Cms.Core.Scoping
#endif

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