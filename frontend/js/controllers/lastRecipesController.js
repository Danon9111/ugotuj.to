/*
lastRecipesController - The controller gives the ability to show last recipes added to the database.
*/

app.controller('lastRecipesController',['$scope', '$http', '$route', '$rootScope', '$routeParams', function($scope, $http, $route, $rootScope, $routeParams) {
  $rootScope.bgImage = "";

  $scope.lastRecipes = {};

  $http.get('/api/latestrecipe')
    .success(function(res) {
      $scope.lastRecipes = res;
      $scope.slides = [];
      for(var recipe in res) {
        if($scope.lastRecipes[recipe].photo == "" || $scope.lastRecipes[recipe].photo == "img/nophoto.jpg") {
          $scope.lastRecipes[recipe].photo = 'img/woodenBg_8.jpg';
        }
      }
      $scope.slides = res;
    })

  $scope.myInterval = 5000;
  $scope.noWrapSlides = false;
}]);
