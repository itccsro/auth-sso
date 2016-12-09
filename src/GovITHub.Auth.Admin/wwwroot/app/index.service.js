(function () {
    'use strict';
    angular.module('authAdminPanel')
    .service('UserIdentityService', ['$q', '$http', '$rootScope', function ($q, $http, $rootScope) {

        var getUser = function () {
            return $http.get('/api/useridentity').then(function (response) {
                $rootScope.currentUser = response.data;
                return response.data;
            });
        };
        return {
            getUser: getUser
        };
    }]);    
})();

