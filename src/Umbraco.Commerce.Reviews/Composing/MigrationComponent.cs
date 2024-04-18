using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

using Umbraco.Commerce.Reviews.Migrations.V_1_0_0;

namespace Umbraco.Commerce.Reviews.Composing
{
    internal class MigrationComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMigrationBuilder _migrationBuilder;
        private readonly IKeyValueService _keyValueService;

        private readonly IMigrationPlanExecutor _migrationPlanExecutor;

        public MigrationComponent(
            IScopeProvider scopeProvider,
            IMigrationBuilder migrationBuilder,
            IMigrationPlanExecutor migrationPlanExecutor,
            IKeyValueService keyValueService)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _migrationPlanExecutor = migrationPlanExecutor;
            _keyValueService = keyValueService;
        }

        public void Initialize()
        {
            var plan = new MigrationPlan(Constants.System.MigrationPlanName);

            plan.From(string.Empty)
                // 1.0.0
                .To<CreateReviewTable>("c0e73d2a-bdae-4b3b-9742-ff27fb8233c2")
                .To<CreateReviewCommentTable>("9d777d60-2e2c-4c67-a49a-e725170abbdf");

            new Upgrader(plan)
                .Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
        }

        public void Terminate()
        { }
    }
}