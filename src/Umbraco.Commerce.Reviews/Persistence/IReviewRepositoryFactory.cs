using Umbraco.Commerce.Common;
using Umbraco.Commerce.Reviews.Persistence.Repositories;

namespace Umbraco.Commerce.Reviews.Persistence
{
    public interface IReviewRepositoryFactory
    {
        IReviewRepository CreateReviewRepository(IUnitOfWork uow);
    }
}
