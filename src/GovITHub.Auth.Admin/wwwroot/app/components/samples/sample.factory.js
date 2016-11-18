(function () {
    'use strict';

    angular
      .module('authAdminPanel')
      .factory('Sample', ['$q', '$resource', '$log', function ($q, $resource, $log) {
              return $resource('/api/sample/:id', {}, {
                  query: { method: 'GET', params: { id: '' }, isArray: true },
                  post: { method: 'POST' },
                  update: { method: 'PUT', params: { id: '@id' } },
                  remove: { method: 'DELETE' }
              });
      }]);
})();

