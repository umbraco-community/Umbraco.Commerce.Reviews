using Umbraco.Core.Migrations;

#if NETFRAMEWORK
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Umbraco.Core.Persistence.SqlSyntax;
#else
using Umbraco.Cms.Infrastructure.Persistence.DatabaseModelDefinitions;
using Umbraco.Cms.Infrastructure.Persistence.SqlSyntax;
#endif

namespace Vendr.Contrib.Reviews.Migrations.V_1_0_0
{
    public class CreateReviewCommentTable : MigrationBase
    {
        public CreateReviewCommentTable(IMigrationContext context) 
            : base(context) 
        { }

        public override void Migrate()
        {
            var commentTableName = VendrReviewsConstants.DatabaseSchema.Tables.Comment;
            var reviewTableName = VendrReviewsConstants.DatabaseSchema.Tables.Review;
            var storeTableName = Vendr.Infrastructure.Constants.DatabaseSchema.Tables.Store;

            if (!TableExists(commentTableName))
            {
                var nvarcharMaxType = SqlSyntax is SqlCeSyntaxProvider
                    ? "NTEXT"
                    : "NVARCHAR(MAX)";

                // Create table
                Create.Table(commentTableName)
                    .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey($"PK_{commentTableName}")
                    .WithColumn("storeId").AsGuid().NotNullable()
                    .WithColumn("reviewId").AsGuid().NotNullable()
                    .WithColumn("body").AsCustom(nvarcharMaxType).NotNullable()
                    .WithColumn("createDate").AsDateTime().NotNullable()
                    .Do();

                // Foreign key constraints
                Create.ForeignKey($"FK_{commentTableName}_{storeTableName}")
                    .FromTable(commentTableName).ForeignColumn("storeId")
                    .ToTable(storeTableName).PrimaryColumn("id")
                    .Do();

                Create.ForeignKey($"FK_{commentTableName}_{reviewTableName}")
                    .FromTable(commentTableName).ForeignColumn("reviewId")
                    .ToTable(reviewTableName).PrimaryColumn("id")
                    .Do();
            }
        }
    }
}