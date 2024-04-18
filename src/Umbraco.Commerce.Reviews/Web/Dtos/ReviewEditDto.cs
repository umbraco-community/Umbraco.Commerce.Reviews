﻿using System.Runtime.Serialization;
using Notification = Umbraco.Cms.Core.Models.ContentEditing.BackOfficeNotification;

namespace Umbraco.Commerce.Reviews.Web.Dtos
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