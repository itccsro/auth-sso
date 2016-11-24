(function () {
    'use strict';
    /*eslint angular/di: [2,"array"]*/
    angular
        .module('authAdminPanel')
        .controller('OrganizationsEditController', ["Organization", "$state", "$scope", 'status', 'id',

        function (Sample, $state, $scope, status, id) {
            var vm = this,
                vmLocal = {};

            vm.data = {
                isComplete: false
            };
            vm.status = status;

            vm.id = id;

            var create = function () {
                Sample
                    .save(vm.data).$promise
                    .then(function (result) {
                        $state.go('index.samples');
                    }).catch(function (err) {
                        vm.error = err;
                        console.error(err);
                    });
            };

            var update = function () {
                Sample
                    .update({ id: vm.id }, vm.data).$promise
                    .then(function (result) {
                        $state.go('index.samples');
                    }).catch(function (err) {
                        vm.error = err;
                        console.error(err);
                    });
            }

            vm.save = function () {
                if (vm.status.edit && vm.id) {
                    update();
                } else {
                    create();
                }
            };

            vm.init = function () {
                Sample.get({ id: vm.id }).$promise
                    .then(function (result) {
                        vm.data = result;
                    }).catch(function (err) {
                        vm.error = err;
                        console.error(err);
                    });
            };

            if (vm.status.edit) {
                vm.init();
            };



            // -->End
            $scope.$on('$destroy', function () {
                vm.data = {};
                vmLocal = null;
            });
        }
        ]);
})();
