(function() {
    'use strict';
/*eslint angular/di: [2,"array"]*/

angular.module('authAdminPanel')
.controller('articlesListController', ['$scope', 'resourceManager', '$log', '$rootScope', 'UserIdentityService',
    function ($scope, resourceManager, $log, $rootScope, UserIdentityService) {
    var vm = this,
        vmLocal = {};
    vm.data = []



    $scope.$on("$destroy", function() {
        vmLocal = null;
    })
}]);
})();