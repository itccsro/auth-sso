(function () {
    'use strict';

    angular
      .module('authAdminPanel')
      .factory('Organization', ['$q', '$resource', '$log', function ($q, $resource, $log) {
          return $resource('/api/organizations/:id', { id: '@id' }, {
              filter: { method: 'GET' }, 
              update: { method: 'PUT'}
          });
      }]);
})();

