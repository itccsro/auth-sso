(function () {
    'use strict';

    var module = angular.module("authAdminPanel");

    // this code can be used with uglify
    module.controller("AuthorizeController",
		[
            "$log",
            "$scope",
            "SecurityService",
			AuthorizeController
		]
	);

    function AuthorizeController($log, $scope, SecurityService) {
        $log.info("AuthorizeController called");
        $scope.message = "AuthorizeController created";

        SecurityService.DoAuthorization();
    }
})();