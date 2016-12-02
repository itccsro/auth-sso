'use strict';

angular.module('authAdminPanel')
  .controller('MainController',['User', function (User) { 

    var vm = this;

    vm.userName = User.data.name;
    vm.helloText = 'Welcome in Admin Auth Panel';
    vm.descriptionText = 'This is a Angular web-app';

  }]);
