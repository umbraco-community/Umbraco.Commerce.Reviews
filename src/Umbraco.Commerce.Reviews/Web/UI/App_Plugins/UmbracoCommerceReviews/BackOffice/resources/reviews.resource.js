(function() {

    'use strict';

    function commerceReviewsResource($http, umbRequestHelper) {

        return {

            getReview: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetReview", { params: { id: id } }),
                    "Failed to get review");
            },

            getReviews: function (ids) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetReviews", { params: { ids: ids } }),
                    "Failed to get reviews");
            },

            getReviewsForProduct: function (storeId, productReference) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetReviewsForProduct", {
                        params: {
                            storeId: storeId,
                            productReference: productReference
                        }
                    }),
                    "Failed to get reviews for product");
            },

            getReviewsForCustomer: function (storeId, customerReference) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetReviewsForCustomer", {
                        params: {
                            storeId: storeId,
                            customerReference: customerReference
                        }
                    }),
                    "Failed to get reviews for customer");
            },

            searchReviews: function (storeId, opts) {

                var params = angular.extend({}, {
                    storeId: storeId
                }, opts);

                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/SearchReviews", { params: params }),
                    "Failed to search reviews");
            },

            saveReview: function (review) {
                return umbRequestHelper.resourcePromise(
                    $http.post("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/SaveReview", review),
                    "Failed to save review");
            },

            deleteReview: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.delete("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/DeleteReview", { params: { id: id } } ),
                    "Failed to delete review");
            },

            changeReviewStatus: function (reviewId, status) {
                return umbRequestHelper.resourcePromise(
                    $http.post("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/ChangeReviewStatus", {
                        reviewId: reviewId,
                        status: status
                    }),
                    "Failed to change review status");
            },

            getProductData: function (productReference, languageIsoCode) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetProductData", {
                        params: {
                            productReference: productReference,
                            languageIsoCode: languageIsoCode
                        }
                    }), "Failed to get product data");
            },

            getReviewStatuses: function (storeId) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/GetReviewStatuses", {
                        params: {
                            storeId: storeId
                        }
                    }), "Failed to get review statuses");
            },

            saveComment: function (id, storeId, reviewId, body) {
                return umbRequestHelper.resourcePromise(
                    $http.post("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/SaveComment", {
                        id: id,
                        storeId: storeId,
                        reviewId: reviewId,
                        body: body
                    }), "Failed to add comment");
            },

            deleteComment: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.delete("/umbraco/backoffice/UmbracoCommerceReviews/ReviewApi/DeleteComment", { params: { id: id } }),
                    "Failed to delete comment");
            },

        };

    }

    angular.module('umbraco.commerce.resources').factory('commerceReviewsResource', commerceReviewsResource);

}());
