/*
showrecipevalidate directive chooses to show result view or the error one.
*/

app.directive('showrecipevalidate', ['$cookies', '$route', '$http', function($cookies, $route, $http) {
  return {
    restrict: 'A',
    link: function($scope) {

      $http.get("http://ugotuj.to.hostingasp.pl/api/Recipe/" + $route.current.params.recipeId)
      .success(function (data) {
        if(data["error"] == null) {
          $scope.allow = true;
        } else {
          $scope.allow = false;
        }
        //console.log(data['error']);
      })

      $scope.show = function() {
        if($scope.allow == true) {
          return 'js/directives/showrecipevalidate.html';
        } else if($scope.allow == false) {
          return 'js/directives/error404.html';
        }
      }

    },
    template: '<div ng-include="show()"></div>'
  }
}]);
