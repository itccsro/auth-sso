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

