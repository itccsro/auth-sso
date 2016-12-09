(function () {
  'use strict';
  /*eslint angular/di: [2,"array"]*/
  angular.module('authAdminPanel')
    .controller('votingStationsUpdateController', ['resourceManager', '$rootScope', '$log', '$scope', 'articleId', 'UserUserIdentityService',
      function (resourceManager, $rootScope, $log, $scope, articleId, UserIdentityService) {
        var vm = this,
          vmLocal = {};

        // -->End
        $scope.$on('$destroy', function () {
           
          vm.status = {};
          vmLocal = null;
        })
      }
    ]);
})();
