(function () {
    'use strict';
    /*eslint angular/di: [2,"array"]*/
    angular.module('authAdminPanel')
    .controller('articlesNewController', ['resourceManager', '$rootScope', '$log', '$scope', 'articleId', 'UserIdentityService',
        function (resourceManager, $rootScope, $log, $scope, articleId, UserIdentityService) {
            var vm = this,
                vmLocal = {};

            // -->End
            $scope.$on('$destroy', function () {
                vm.status = {};
                vmLocal = null;
            })
        }]);
})();