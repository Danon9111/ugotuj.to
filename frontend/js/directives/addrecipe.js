/*
addrecipe directive chooses to show result view or the error one.
*/


app.directive('addrecipe', ['$cookies', 'validateAuthToken', function($cookies, validateAuthToken) {
  return {
    restrict: 'E',
    link: function($scope) {

      validateAuthToken.success(function(res) {
        $scope.validateAuthToken = res;
      });

      $scope.getContentUrl = function() {
        if($scope.validateAuthToken) {
          return 'js/directives/addrecipe.html';
        } else {
          return 'js/directives/error404.html';
        }
      }
    },
    template: '<div ng-include="getContentUrl()"></div>'
  }
}]);
