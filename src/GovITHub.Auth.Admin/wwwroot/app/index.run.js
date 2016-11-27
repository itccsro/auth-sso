(function() {
  'use strict';

  angular
    .module('authAdminPanel')
    .run(function ($log, $rootScope, $state, SecurityService) {
          /** @ngInject */
          $rootScope.goto = function(route, opts) {
              $state.go(route,opts);
          }

        // check if current location matches route  
        var routesThatDontRequireAuth = ['/authorized','/unauthorized'];
        var routeClean = function (route) {
            return _.find(routesThatDontRequireAuth,
              function (noAuthRoute) {
                  return route.startsWith(noAuthRoute);
              });
        };

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            if (!routeClean(toState.url) && !SecurityService.IsAuthorized()) {
                event.preventDefault();
                $state.go("unauthorized");
            }
        })

        $log.debug('runBlock end');
      });
})();
