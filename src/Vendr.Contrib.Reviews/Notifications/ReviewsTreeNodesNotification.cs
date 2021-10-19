#if NET
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Notifications;

namespace Vendr.Contrib.Reviews.Notifications
{
    public class ReviewsTreeNodesNotification : INotificationHandler<TreeNodesRenderingNotification>
    {
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;

        public ReviewsTreeNodesNotification(IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
        {
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        }

        public void Handle(TreeNodesRenderingNotification notification)
        {
            if (notification.TreeAlias == Vendr.Core.Constants.System.ProductAlias
                && notification.QueryString["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var mainRoute = Vendr.Umbraco.Constants.Sections.Commerce + "/" + Constants.System.ProductAlias;

                var storeId = notification.QueryString["id"];
                var id = Constants.Trees.Reviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, notification.QueryString, "Reviews", Constants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.Reviews.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Umbraco.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Umbraco.Constants.Sections.Commerce);

                var optNodeIndex = notification.Nodes.FindIndex(x => x.NodeType == "Options");
                var index = optNodeIndex >= 0 ? optNodeIndex : notification.Nodes.Count;

                notification.Nodes.Insert(index, reviewsNode);
            }

        }
    }
}

#endif