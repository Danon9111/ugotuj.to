/*
searchController - The controller gives the ability to search through the database for specific recipes.
*/

app.controller('searchController', ['$scope', '$http', '$rootScope', '$cookies', function($scope, $http, $rootScope, $cookies) {

  $rootScope.bgImage = "";

  $scope.searchWords = "";
  $scope.searchResults = "";
  $scope.specialImage = true;
  $scope.searchRepeat = false;

  if($scope.searchWords.length < 1) {
    $scope.searchRepeat = false;
  } else {
    $scope.searchRepeat = true;
  }

  $scope.updateResults = function() {
    if($scope.searchWords == "") {
      $scope.searchResults = {};
      $scope.loading = false;
      $scope.specialImage = true;
    } else {
      $scope.loading = true;
      $scope.specialImage = false;
      $http.get('/api/SearchRecipe?query=' + $scope.searchWords)
        .success(function(res) {
          $scope.searchResults = res;
        })
        .error(function(res) {
          console.log('Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.');
        })
        .finally(function(){
          $scope.loading = false;
        });
      }
  }
}]);
