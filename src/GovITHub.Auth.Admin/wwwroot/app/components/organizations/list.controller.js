(function () {
    'use strict';
    /*eslint angular/di: [2,"array"]*/
    angular
        .module('authAdminPanel')
        .controller('OrganizationsListController', ['Organization', '$rootScope', '$log', '$scope', 'User',
            function (Sample, $rootScope, $log, $scope, User) {
                var vm = this,
                    vmLocal = {};

                vm.pagination = {
                    currentPage: 1,
                    itemsPerPage: 7,
                    totalItems: 150,
                    maxDisplayedPages: 5
                };
                vm.sortBy = 'name';
                vm.sortAscending = true;

                vm.search = function () {
                    Sample.filter({
                        q: vm.query, // {name: "test search"}
                        currentPage: vm.pagination.currentPage, // 1
                        itemsPerPage: vm.pagination.itemsPerPage, // 50
                        sortBy: vm.sortBy, // 'name'
                        sortAscending: vm.sortAscending // true  OR false
                    }).$promise
                        .then(function (result) {
                            vm.items = result.list;
                            vm.pagination.totalItems = result.totalItems
                        }).catch(function (err) {
                            $log.error(err);
                            vm.error = err;
                        });
                };

                vm.sort = function (sortBy) {
                    vm.sortBy = sortBy;
                    vm.sortAscending = !vm.sortAscending;
                    vm.search();
                };

                vm.gotoEdit = function (id) {
                    $rootScope.goto('index.samples_edit', { id: id });
                };

                vm.delete = function (id) {
                    Sample.delete({ id: id }).$promise
                        .then(function (response) {
                            vm.search();
                        }).catch(function (err) {
                            $log.error(err);
                            vm.error = err;
                        });
                };

                vm.search();


                // -->End
                $scope.$on('$destroy', function () {
                    vmLocal = null;
                })
            }]);
})();