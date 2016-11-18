(function () {
    'use strict';
    /*eslint angular/di: [2,"array"]*/
    angular.module('SampleNewController', ["Sample", "$state", "$scope",

        function (Sample, $state, $scope) {
            var vm = this;
            vm.create = create;
            vm.data = {
                isComplete: false
            };

            function create() {
                vmLocal.SampleResource.$save(vm.data);

                vmLocal.SampleResource.$promise
                    .then(function (result) {
                        $state.go('index.samples');
                    }).catch(function (err) {
                        vm.error = err;
                        console.error(err);
                    });
            }

            vmLocal.SampleResource = new Sample();

            // -->End
            $scope.$on('$destroy', function () {
                vm.data = {};
                vmLocal = null;
            })
        }
    ]);
})();
