(function() {
  'use strict';

  angular
    .module('authAdminPanel')
    .run(function ($log, $rootScope, $state, UserIdentityService) {
          /** @ngInject */
          $rootScope.goto = function(route, opts) {
              $state.go(route,opts);
          }

          $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
              if ($rootScope.currentUser &&
                  $rootScope.currentUser.organizationId < 1 &&
                  toState.name != 'index.organization_new') {
                  event.preventDefault();
                  $state.go("index.organization_new");
              }
          });

          $log.debug('runBlock end');
      });
})();
