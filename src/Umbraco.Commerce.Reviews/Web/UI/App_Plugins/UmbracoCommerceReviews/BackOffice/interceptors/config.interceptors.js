(function () {
    'use strict';

    angular.module('umbraco.commerce.interceptors')
        .config(function ($httpProvider) {
            $httpProvider.interceptors.push('commerceReviewsRouteRewritesInterceptor');
        });
})();
