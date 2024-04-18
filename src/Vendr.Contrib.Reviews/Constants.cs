﻿namespace Umbraco.Commerce.Reviews
{
    /// <summary>
    /// Constants all the identifiers
    /// </summary>
    public static partial class Constants
    {
        internal static partial class Internals
        {
            public const string PluginControllerName = "UmbracoCommerceReviews";
        }

        public static class System
        {
            public const string ProductAlias = "umbracoCommerceReviews"; 
            
            public const string ProductName = "UmbracoCommerceReviews";

            public const string MigrationPlanName = "Umbraco.Commerce.Reviews";
        }

        public static class DatabaseSchema
        {
            public const string TableNamePrefix = "umbracoCommerce";

            public static class Tables
            {
                public const string Review = TableNamePrefix + "Review";

                public const string Comment = TableNamePrefix + "ReviewComment";
            }
        }

        public static class Trees
        {
            public static class Reviews
            {
                /// <summary>
                /// Alias for reviews node
                /// </summary>
                public const string Alias = "reviews";

                /// <summary>
                /// Id for reviews node
                /// </summary>
                public const string Id = "100";

                /// <summary>
                /// System reviews icon
                /// </summary>
                public const string Icon = "icon-rate";

                /// <summary>
                /// System reviews node type
                /// </summary>
                public const string NodeType = "Review";
            }
        }

        public static class Entities
        {
            public static class EntityTypes
            {
                /// <summary>
                /// Review entity type
                /// </summary>
                public const string Review = "Review";
            }
        }

    }
}