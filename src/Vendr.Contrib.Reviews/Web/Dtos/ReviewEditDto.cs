using System.Collections.Generic;
using System.Runtime.Serialization;

#if NETFRAMEWORK
using Notification = Umbraco.Web.Models.ContentEditing.Notification;
#else
using Notification = Umbraco.Cms.Core.Models.ContentEditing.BackOfficeNotification;
#endif

namespace Vendr.Contrib.Reviews.Web.Dtos
{
    [DataContract(Name = "review", Namespace = "")]
    public class ReviewEditDto : ReviewDto
    {
        [DataMember(Name = "path")]
        public string[] Path => new string[] { "-1", StoreId.ToString(), Constants.Trees.Reviews.Id, Id.ToString() };

        [DataMember(Name = "notifications")]
        public List<Notification> Notifications { get; set; }

        public ReviewEditDto()
        {
            Notifications = new List<Notification>();
        }
    }
}