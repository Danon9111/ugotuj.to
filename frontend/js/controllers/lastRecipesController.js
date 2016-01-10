/*
lastRecipesController - The controller gives the ability to show last recipes added to the database.
*/

app.controller('lastRecipesController',['$scope', '$http', '$route', '$rootScope', '$routeParams', function($scope, $http, $route, $rootScope, $routeParams) {
  $rootScope.bgImage = "";

  $scope.lastRecipes = {};

  $http.get('http://ugotuj.to.hostingasp.pl/api/latestrecipe')
    .success(function(res) {
      $scope.lastRecipes = res;
      $scope.slides = [];
    $scope.slides = res;
    })

  $scope.myInterval = 5000;
  $scope.noWrapSlides = false;
}]);
