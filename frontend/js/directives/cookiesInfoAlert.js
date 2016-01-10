/*
cookiesInfoAlert directive handles the info about storing cookies message.
*/

app.directive('cookiesInfoAlert', ['$cookies', '$localStorage', function($cookies, $localStorage) {
  return {
    restrict: 'A',
    link: function($scope) {

      $scope.hideCookiesAlert = function() {
        $localStorage.put('cookiesAlert', 'true', 365);
      }

      $scope.hasClosedAlert = function() {
        if($localStorage.get('cookiesAlert') == null || $localStorage.get('cookiesAlert') == "") {
          return 'js/directives/cookiesInfoAlert.html';
        } else if($localStorage.get('cookiesAlert') == true) {
          //nop.
        }
      }
    },
    template: '<div ng-include="hasClosedAlert()"></div>'
  }
}]);
