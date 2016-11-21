(function () {
  'use strict';
  /*eslint angular/di: [2,"array"]*/
  angular.module('authAdminPanel')
    .controller('votingStationsUpdateController', ['resourceManager', '$rootScope', '$log', '$scope', 'articleId', 'User',
      function (resourceManager, $rootScope, $log, $scope, articleId, User) {
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
