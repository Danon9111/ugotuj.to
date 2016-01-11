/*
randomRecipeController - The controller gives the ability to get a random recipe from the database.
*/

app.controller('randomRecipeController',['$scope', '$http', '$route', '$rootScope', '$routeParams', function($scope, $http, $route, $rootScope, $routeParams) {
  $rootScope.bgImage = "";

  $scope.randomRecipe = {};
  $http.get('/api/randomrecipe')

    .success(function(res) {
      $scope.randomRecipe = res;
    })
}]);
