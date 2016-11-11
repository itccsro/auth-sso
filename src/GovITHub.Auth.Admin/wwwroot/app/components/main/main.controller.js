'use strict';

angular.module('authAdminPanel')
  .controller('MainController', function () { 

    var vm = this;

    vm.userName = 'Example user';
    vm.helloText = 'Welcome in Admin Auth Panel';
    vm.descriptionText = 'This is a Angular web-app';

  });
