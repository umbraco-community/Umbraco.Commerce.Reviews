using Vendr.Common.Events;
using Vendr.Contrib.Reviews.Models;

namespace Vendr.Contrib.Reviews.Events
{
    public class ReviewNotificationBase : NotificationEventBase
    {
        public Review Review { get; }

        public ReviewNotificationBase(Review review)
        {
            Review = review;
        }
    }

    public class ReviewAddingNotification : ReviewNotificationBase
    {
        public ReviewAddingNotification(Review review)
          : base(review)
        { }
    }

    public class ReviewAddedNotification : ReviewNotificationBase
    {
        public ReviewAddedNotification(Review review)
          : base(review)
        { }
    }
}