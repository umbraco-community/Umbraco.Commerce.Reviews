using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.ModelBinders;

namespace Umbraco.Commerce.Reviews.Web.Controllers
{
    [Tree(
        Umbraco.Commerce.Cms.Constants.Sections.Commerce,
        Constants.Trees.Reviews.Alias,
        //TreeGroup = "commerce",
        TreeTitle = "Reviews",
        SortOrder = 10,
        TreeUse = TreeUse.None)]
    [PluginController(Constants.Internals.PluginControllerName)]
    public sealed class ReviewTreeController : TreeController
    {
        private readonly ILocalizedTextService _localizedTextService;
        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

        public ReviewTreeController(
            ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
            IEventAggregator eventAggregator,
            IMenuItemCollectionFactory menuItemCollectionFactory)
            : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _localizedTextService = localizedTextService;
            _menuItemCollectionFactory = menuItemCollectionFactory;
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var menu = _menuItemCollectionFactory.Create();

            menu.Items.Add<ActionDelete>(_localizedTextService).LaunchDialogView("/app_plugins/vendrreviews/backoffice/views/dialogs/delete.html", "Delete");

            return menu;
        }

        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings) => null;
    }
}