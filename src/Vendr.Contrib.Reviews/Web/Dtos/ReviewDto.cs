﻿using System.Runtime.Serialization;
using Umbraco.Commerce.Reviews.Persistence.Dtos;

namespace Umbraco.Commerce.Reviews.Web.Dtos
{
    [DataContract(Name = "review", Namespace = "")]
    public class ReviewDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "productReference")]
        public string ProductReference { get; set; }

        [DataMember(Name = "customerReference")]
        public string CustomerReference { get; set; }

        [DataMember(Name = "rating")]
        public decimal Rating { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "verifiedBuyer")]
        public bool VerifiedBuyer { get; set; }

        [DataMember(Name = "recommendProduct")]
        public bool? RecommendProduct { get; set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "updateDate")]
        public DateTime UpdateDate { get; set; }

        [DataMember(Name = "status")]
        public ReviewStatusDto Status { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "comments")]
        public List<CommentDto> Comments { get; set; }

        public ReviewDto()
        {
            Icon = "icon-rate";
        }
    }
}