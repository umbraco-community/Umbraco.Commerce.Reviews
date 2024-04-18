using System.Runtime.Serialization;
using Umbraco.Commerce.Reviews.Models;

namespace Umbraco.Commerce.Reviews.Web.Dtos
{
    [DataContract(Name = "changeReviewStatus", Namespace = "")]
    public class ChangeReviewStatusDto
    {
        [DataMember(Name = "reviewId")]
        public Guid ReviewId { get; set; }

        [DataMember(Name = "status")]
        public ReviewStatus Status { get; set; }
    }
}