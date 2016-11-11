(function() {
  'use strict';
/*eslint angular/di: [2,"array"]*/

angular.module('authAdminPanel')
.controller('articlesDashboardController', ['resourceManager', '$scope', '$rootScope', '$log', 'articleId', 
    function (resourceManager, $scope, $rootScope, $log, articleId) { 
        var vm = this,
            vmLocal = {};
        
        vm.data = {}

        $log.info(articleId); 
        
        // TODO:: fetch the list of companies from the server and display here
        $scope.$on("$destroy", function() {
            vmLocal = null;
        })
    }]);
})();
