/*
menulinks directive chooses to show specific links in menu navigation bar.
*/


app.directive('menulinks', ['$cookies', 'validateAuthToken', function($cookies, validateAuthToken) {
  return {
    restrict: 'E',
    link: function($scope) {

      validateAuthToken.success(function(res) {
        $scope.validateAuthToken = res;
      });

      $scope.getLinks = function() {
        if($scope.validateAuthToken) {
          return 'js/directives/loggedinlinks.html';
        } else {
          return 'js/directives/incognitolinks.html';
        }
      }
    },
    template: '<div ng-include="getLinks()"></div>'
  }
}]);
