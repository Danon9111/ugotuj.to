/*
changepasswordvalidate directive chooses to show result view or the error one.
*/

app.directive('changepasswordvalidate', ['$cookies', 'validateAuthToken', function($cookies, validateAuthToken) {
  return {
    restrict: 'A',
    link: function($scope) {

      validateAuthToken.success(function(res) {
        $scope.validateAuthToken = res;
      });

      $scope.getContentUrl = function() {
        if($scope.validateAuthToken) {
          return 'js/directives/changepasswordvalidate.html';
        } else {
          return 'js/directives/error403.html';
        }
      }
    },
    template: '<div ng-include="getContentUrl()"></div>'
  }
}]);
