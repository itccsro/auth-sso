/*

use as attribute
Example:

<button class="btn-danger btn btn-xs " request-confirmation="Sunteti sigur?" confirmed-action="vm.remove(id)" canceld-action="vm.cancel()"> Sterge</button>

*/

(function () {
  'use strict';

  angular
    .module('authAdminPanel')
    .directive('requestConfirmation', requestConfirm);

  /* @ngInject */
  function requestConfirm($uibModal) {
    var directive = {
      restrict: 'A',
      link: linkFunc,
    };

    return directive;

    function linkFunc(scope, element, attr, ctrl) {
      var confirmQuestion = attr.requestConfirmation || "Sunteți sigur de aceasta acțiune?";
      var confirmedAction = attr.confirmedAction;
      var canceldAction = attr.canceldAction;

      element.bind('click', function (event) {

        var modalInstance = $uibModal.open({
          ariaLabelledBy: 'modal-title',
          ariaDescribedBy: 'modal-body',
          template: '<div class="modal-header"><h3 class="modal-title" id="modal-title">{{$ctrl.confirmQuestion}}</h3></div><div class="modal-footer">	<button class="btn btn-danger" type="button" ng-click="$ctrl.confirm()">Confirmă</button>	<button class="btn btn-warning" type="button" ng-click="$ctrl.cancel()">Anulează</button></div>',
          size: 'sm',
          controllerAs: '$ctrl',
          resolve: {
            confirmQuestion: function () {
              return confirmQuestion;
            }
          },
          controller: function ($uibModalInstance, confirmQuestion) {
            var $ctrl = this;
            $ctrl.confirmQuestion = confirmQuestion;
            $ctrl.confirm = function () {
              $uibModalInstance.close();
            };
            $ctrl.cancel = function () {
              $uibModalInstance.dismiss();
            };
          }
        });

        modalInstance.result.then(function (selectedItem) {
          if (confirmedAction) {
            scope.$eval(confirmedAction)
          }
          scope.$eval()
        }, function () {
          if (canceldAction) {
            scope.$eval(canceldAction)
          }
        });
      });
    }
  }

})();
