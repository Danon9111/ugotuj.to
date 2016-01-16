/*
accountController - The controller gives the ability manage users' account.
*/


app.controller('accountController',['$scope', '$http', '$route', '$rootScope', '$routeParams', '$cookies', 'validateAuthToken', '$timeout', '$q', 'Notifications', function($scope, $http, $route, $rootScope, $routeParams, $cookies, validateAuthToken, $timeout, $q, Notifications) {
  $rootScope.meta.title = "Moje konto";
  $rootScope.bgImage = "";

  $scope.user = {};
  $rootScope.userRecipes = {};

  if($scope.user.profilephoto == null) {
    $scope.user.profilephoto = 'img/avatar.png';
  }

  $scope.notificationService = Notifications;
  $scope.notificationService.all = [];

  validateAuthToken.success(function(res) {
    $scope.validateAuthToken = res;
    $scope.validAuth = res;
  });

  /* Retrieve user profilephoto */
  $scope.retrieveUserPhoto = function() {
    $http.get('/api/profilephoto?token=' + $cookies.authToken)
    .success(function(res) {
      if(res == null) {
        $scope.user.profilephoto = 'img/avatar.png';
      }
      $scope.user.profilephoto = res;
    })
    .error(function(res) {
      $scope.user.profilephoto = 'img/avatar.png';
    });
  }
  /* Retrieve user profilephoto end */

  /* upload File -> user profilephoto */
  $scope.uploadUserPhoto = function(files) {
      var fd = new FormData();
      //Take the first selected file
      fd.append("file", files[0]);
      $scope.loading = true;
      if(files[0].size >= 1000000) {
        $scope.loading = false;
        files = null;
        fd = null;
        //$scope.notificationService.Add("alert", "Rozmiar pliku jest za duży.");
      } else {
        $http.post('/api/profilephoto?token=' + $cookies.authToken, fd, {
            withCredentials: true,
            headers: {'Content-Type': undefined },
            transformRequest: angular.identity
        })
        .success(function(res){
          $scope.loading = false;
          $scope.retrieveUserPhoto();
          $scope.notificationService.Add("success", "Awatar został zmieniony!");
        })
        .error(function(res) {
          $scope.loading = false;
          $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
        });
      }
  };
  /* upload File -> user profilephoto end */

  $scope.retrieveFavouriteRecipes = function() {
    $http.post('/api/GetFavoriteRecipes', {token: $cookies.authToken}).success(function(res) {
      if(res.recipes == "" || res.recipes == null) {
        $scope.isFavouritesEmpty = true;
      } else {
        $scope.favourite_recipes = res.recipes;
        $scope.isFavouritesEmpty = false;
      }
    });
  }

  $scope.retrieveFavouriteRecipes();

  /* Remove recipe by ID */
  $scope.removeRecipeById = function(id) {
    $http.post('/api/remove', { token: $cookies.authToken, id: id })
    .success(function(res) {
      console.log('Done it!');
    })

    $http.post('/api/userprofile', { token: $cookies.authToken })
    .success(function(res) {
      $rootScope.userRecipes = res["recipes"];
      if(res["recipes"] == null || res["recipes"] == "") {
        $scope.isRecipeEmpty = true;
      } else {
        $scope.isRecipeEmpty = false;
      }
    })
  };
  /* Remove recipe by ID end */

  /* Retrieve recipes written by user */
  if($scope.validateAuthToken != true) {
    window.location="/account/login";
  } else {

    $scope.retrieveUserPhoto();

    $http.post('/api/userprofile', { token: $cookies.authToken })
    .success(function(res) {
      $scope.user = res;
      $rootScope.userRecipes = res["recipes"];
      if(res["recipes"] == null || res["recipes"] == "") {
        $scope.isRecipeEmpty = true;
      } else {
        $scope.isRecipeEmpty = false;
      }
    })
    .error(function(res) {
      $scope.notificationService.Add("alert", "Ups! :( Nie można probrać Twoich danych.");
    })
  }
  /* Retrieve recipes written by user end */

}]);
