using Vendr.Contrib.Reviews.Api;

#if NETFRAMEWORK
using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
#else
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.BackOffice.Trees;
#endif

namespace Vendr.Contrib.Reviews.Composing
{
    public class VendrReviewsComponent : IComponent
    {
        private readonly VendrReviewsApi _api;

        public VendrReviewsComponent(VendrReviewsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            VendrReviewsApi.Instance = _api;

            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        {
            // Unsubscribe on shutdown
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;
        }

        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (sender.TreeAlias == "vendr" && e.QueryStrings["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var mainRoute = "commerce/vendrreviews";

                var storeId = e.QueryStrings["id"];
                var id = VendrReviewsConstants.Trees.Reviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", VendrReviewsConstants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = VendrReviewsConstants.Trees.Reviews.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Umbraco.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Umbraco.Constants.Sections.Commerce);

                var optNodeIndex = e.Nodes.FindIndex(x => x.NodeType == "Options");
                var index = optNodeIndex >= 0 ? optNodeIndex : e.Nodes.Count; 

                e.Nodes.Insert(index, reviewsNode);
            }
        }
    }
}