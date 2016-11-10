(function () {
  'use strict';
  /*eslint angular/di: [2,"array"]*/
  angular.module('authAdminPanel')
    .controller('votingStationsNewController', ["VotingStations", "$state", "$scope",

      function (VotingStations, $state, $scope) {
        var vm = this;
        vm.create = create;
        vm.data = {
          isEnabled: true,
          priority: 100
        };

        function create() {
          var votingStations = new VotingStations()
          votingStations.data = vm.data
          votingStations.$save(votingStations)

          votingStations.$promise
            .then(function (result) {
              $state.go('index.voting_stations');
            }).catch(function (err) {
              vm.error = err;
              console.error(err);
            });
        }

        // -->End
        $scope.$on('$destroy', function () {
          vm.data = {};
        })
      }
    ]);
})();
