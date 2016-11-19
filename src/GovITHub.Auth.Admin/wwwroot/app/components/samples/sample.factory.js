(function () {
    'use strict';

    angular
      .module('authAdminPanel')
      .factory('Sample', ['$q', '$resource', '$log', function ($q, $resource, $log) {
          return $resource('/api/sample/:id', { id: '@id' }, {
              filter: { method: 'GET' }, 
              update: { method: 'PUT'}
          });
      }]);
})();

