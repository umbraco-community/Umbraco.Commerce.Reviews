#if NET
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Trees;
using Url = Umbraco.Cms.Web.BackOffice.Trees.UrlHelperExtensions;

namespace Vendr.Contrib.Reviews.Notifications
{
    public class ReviewsTreeNodesNotification : INotificationHandler<TreeNodesRenderingNotification>
    {
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        private readonly UmbracoApiControllerTypeCollection _apiControllers;

        public ReviewsTreeNodesNotification(
            IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
            UmbracoApiControllerTypeCollection apiControllers)
        {
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
            _apiControllers = apiControllers;
        }

        public void Handle(TreeNodesRenderingNotification notification)
        {
            if (notification.TreeAlias == Vendr.Core.Constants.System.ProductAlias
                && notification.QueryString["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var mainRoute = Vendr.Umbraco.Constants.Sections.Commerce + "/" + Constants.System.ProductAlias;

                var storeId = notification.QueryString["id"];
                var id = Constants.Trees.Reviews.Id;

                //var reviewsNode = CreateTreeNode(id, storeId, notification.QueryString, "Reviews", Constants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                string jsonUrl = Url.GetTreeUrl(_apiControllers, GetType(), id, notification.QueryString);
                string menuUrl = Url.GetMenuUrl(_apiControllers, GetType(), id, notification.QueryString);
                
                var reviewsNode = new TreeNode(id, storeId, jsonUrl, menuUrl)
                {
                    Icon = Constants.Trees.Reviews.Icon,
                    HasChildren = false,
                    Name = "Reviews",
                    Path = $"-1,{storeId},{id}",
                    RoutePath = $"{mainRoute}/review-list/{storeId}",
                    NodeType = Constants.Trees.Reviews.NodeType
                };

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