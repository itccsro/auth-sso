(function () {
    'use strict';

    var module = angular.module("authAdminPanel");

    // this code can be used with uglify
    module.controller("LogoffController",
		[
            "$log",
            "$scope",
            "SecurityService",
			LogoffController
		]
	);

    function LogoffController($log, $scope, SecurityService) {
        $log.info("LogoffController called");
        $scope.message = "LogoffController created";

        SecurityService.Logoff();
    }
})();