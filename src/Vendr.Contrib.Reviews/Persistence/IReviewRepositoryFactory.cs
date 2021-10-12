using Vendr.Common;
using Vendr.Contrib.Reviews.Persistence.Repositories;

namespace Vendr.Contrib.Reviews.Persistence
{
    public interface IReviewRepositoryFactory
    {
        IReviewRepository CreateReviewRepository(IUnitOfWork uow);
    }
}
