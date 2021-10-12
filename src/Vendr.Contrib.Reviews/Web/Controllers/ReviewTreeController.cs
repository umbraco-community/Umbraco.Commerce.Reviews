using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

#if NETFRAMEWORK
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;
#else
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Filters;
#endif

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    [PluginController("VendrReviews")]
    [Tree("commerce", "reviews", TreeTitle = "Reviews", SortOrder = 10, TreeUse = TreeUse.None)]
    public class ReviewTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            menu.Items.Add<ActionDelete>(Services.TextService).LaunchDialogView("/app_plugins/vendrreviews/backoffice/views/dialogs/delete.html", "Delete");

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            throw new System.NotImplementedException();
        }
    }
}