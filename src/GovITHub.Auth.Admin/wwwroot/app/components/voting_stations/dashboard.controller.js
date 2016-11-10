(function () {
  'use strict';
  /*eslint angular/di: [2,"array"]*/

  angular.module('authAdminPanel')
    .controller('votingStationsDashboardController', ['resourceManager', '$scope', '$rootScope', '$log', 'articleId',
      function (resourceManager, $scope, $rootScope, $log, articleId) {
        var vm = this,
          vmLocal = {};


        // TODO:: fetch the list of companies from the server and display here
        $scope.$on("$destroy", function () {
          vmLocal = null;
        })
      }
    ]);
})();
