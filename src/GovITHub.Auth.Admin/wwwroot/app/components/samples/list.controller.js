(function () {
    'use strict';
    /*eslint angular/di: [2,"array"]*/
    angular.module('authAdminPanel')
    .controller('SamplesListController', ['Sample', '$rootScope', '$log', '$scope', 'User',
        function (Sample, $rootScope, $log, $scope, User) {
            var vm = this,
                vmLocal = {};

            vm.query = function () {
                Sample.query().$promise
                    .then(function (result) {
                        vm.items = result;
                    }).catch(function (er) {
                        $log.error(err);
                        vm.error = err;
                    });
            };

            //vmLocal.SampleResource = Sample();

            vm.query();

            // -->End
            $scope.$on('$destroy', function () {
                vmLocal = null;
            })
        }]);
})();