#if NET
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.BackOffice.Trees;
using Vendr.Umbraco.Web.Trees;

namespace Vendr.Contrib.Reviews.Notifications
{
    public class ReviewsTreeNodesNotification : INotificationHandler<TreeNodesRenderingNotification>
    {
        private readonly UmbracoApiControllerTypeCollection _apiControllers;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        //private readonly IUrlHelper _urlHelper;

        public ReviewsTreeNodesNotification(
            UmbracoApiControllerTypeCollection apiControllers,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor
            //IUrlHelper urlHelper
            )
        {
            _apiControllers = apiControllers;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            //_urlHelper = urlHelper;
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

                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                //var urlHelper = new Microsoft.AspNetCore.Mvc.Routing.UrlHelper(_actionContextAccessor.ActionContext);

                string jsonUrl = urlHelper.GetTreeUrl(_apiControllers, typeof(StoresTreeController), id, notification.QueryString);
                string menuUrl = urlHelper.GetMenuUrl(_apiControllers, typeof(StoresTreeController), id, notification.QueryString);
                
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