(function () {
  'use strict';

  angular
    .module('authAdminPanel')
    .factory('VotingStations', votingStations);

  votingStations.$inject = ['$resource', '$log'];

  /* @ngInject */
  function votingStations($resource, $log) {
    return $resource('/api/votingStations/:id', {
      id: '@_id'
    }, {
      update: {
        method: 'PUT' // this method issues a PUT request
      }
    });
  }
})();
