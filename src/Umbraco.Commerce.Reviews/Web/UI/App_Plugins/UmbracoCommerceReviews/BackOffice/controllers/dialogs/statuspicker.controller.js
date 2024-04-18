(function () {

    'use strict';

    function ReviewStatusPickerController($scope, commerceReviewsResource) {

        var defaultConfig = {
            title: "Select a status",
            enableFilter: true,
            orderBy: "sortOrder"
        };

        var vm = this;

        vm.config = angular.extend({}, defaultConfig, $scope.model.config);

        vm.loadItems = function () {
            return commerceReviewsResource.getReviewStatuses(vm.config.storeId);
        };

        vm.select = function (item) {
            $scope.model.value = item;
            if ($scope.model.submit) {
                $scope.model.submit($scope.model.value);
            }
        };

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };
    }

    angular.module('umbraco.commerce').controller('Umbraco.Commerce.Reviews.Controllers.ReviewStatusPickerController', ReviewStatusPickerController);

}());
