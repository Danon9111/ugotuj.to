/*
recommendedRecipeController - The controller gives the ability to get recommended recipe from the database.
*/

app.controller('recommendedRecipeController',['$scope', '$http', '$route', '$rootScope', '$routeParams', function($scope, $http, $route, $rootScope, $routeParams) {
  $rootScope.bgImage = "";

  $scope.recommendedRecipe = {};
  $http.get('http://ugotuj.to.hostingasp.pl/api/randomrecipe')

    .success(function(res) {
      $scope.recommendedRecipe = res;
    })

}]);
