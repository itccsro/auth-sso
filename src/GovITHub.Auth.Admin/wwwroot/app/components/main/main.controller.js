'use strict';

angular.module('authAdminPanel')
  .controller('MainController', ['$scope', '$state', 'UserIdentityService', function ($scope, $state, UserIdentityService) {

      var vm = this;
      UserIdentityService.getUser().then(function (data) {
          vm.userName = data.username;
          if (data.organizationId < 1){
              $state.go("index.organization_new");
              return;
          }
          vm.organizationName = data.organizationName;
          vm.helloText = 'Welcome in Admin Auth Panel';
          vm.descriptionText = 'This is a Angular web-app';
      }, function () {
          $scope.error = 'unable to retrieve the user';
      });
  }]);
