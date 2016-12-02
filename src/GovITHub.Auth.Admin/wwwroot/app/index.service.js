/**
  A service for creating and managing $resource objects for the api 

Default options:
    { 
        'get':    {method:'GET'},
        'save':   {method:'POST'},
        'query':  {method:'GET', isArray:true},
        'remove': {method:'DELETE'},
        'delete': {method:'DELETE'} 
    }; 
 



 */
(function () {
    'use strict';

    //Directive used to set metisMenu and minimalize button
    /*eslint angular/di: [2,"array"]*/
    angular.module('authAdminPanel')
    .service('User', ['$q', function ($q) {
        this.data = {
            name: "Melissa",
            defaults: {
                currency: "GPB"
            },
            lang: "en"
        }
    }]);
    
})();

