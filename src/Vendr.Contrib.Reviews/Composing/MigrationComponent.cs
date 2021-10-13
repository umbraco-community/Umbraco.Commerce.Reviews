#if NETFRAMEWORK
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
#else
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
#endif

using Vendr.Contrib.Reviews.Migrations.V_1_0_0;

namespace Vendr.Contrib.Reviews.Composing
{
    internal class MigrationComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMigrationBuilder _migrationBuilder;
        private readonly IKeyValueService _keyValueService;

#if NETFRAMEWORK
        private readonly ILogger _logger;
#else
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;
#endif

#if NETFRAMEWORK
        public MigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }
#else
        public MigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
        }
#endif

        public void Initialize()
        {
            var plan = new MigrationPlan(Constants.System.MigrationPlanName);

            plan.From(string.Empty)
                // 1.0.0
                .To<CreateReviewTable>("c0e73d2a-bdae-4b3b-9742-ff27fb8233c2")
                .To<CreateReviewCommentTable>("9d777d60-2e2c-4c67-a49a-e725170abbdf");

#if NETFRAMEWORK
            new Upgrader(plan)
                .Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
#else
            new Upgrader(plan)
                .Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
#endif
        }

        public void Terminate()
        { }
    }
}