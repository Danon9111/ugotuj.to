/*
showRecipeController - The controller gives the ability to load specific recipe by a user.
*/

app.controller('showRecipeController',['$scope', '$http', 'Notifications', '$rootScope', '$routeParams', '$cookies', '$route', 'validateAuthToken', '$sce', function($scope, $http, Notifications, $rootScope, $cookies, $routeParams, $route, validateAuthToken, $sce) {

  validateAuthToken.success(function(res) {
    if(res === false) {
      $cookies.authToken = "";
    } else {
      $scope.validateAuthToken = res;
    }
  });

  $rootScope.bgImage = "";
  $rootScope.meta.title = "Przepis";

  $scope.notificationService = Notifications;
  $scope.notificationService.all = [];

  $scope.difficultyImg = "";
  $scope.timeImg = "time_hard";

  $scope.isRecipeFavourite = function() {
    if($routeParams.authToken != null && $routeParams.authToken != "") {
      $http.post('/api/isFavoriteRecipe', { token: $routeParams.authToken, recipeid: $route.current.params.recipeId })
      .success(function(res) {
        $scope.isFav = res;
        return res;
      })
      .error(function(res) {
        $scope.isFav = res;
        return res;
      });
    } else {
      return false;
    }
  }

  $scope.addToFavourites = function(id) {
    $http.post('/api/addFavoriteRecipe', { token: $routeParams.authToken, recipeid: id })
    .success(function(res) {
      $scope.isRecipeFavourite();
    })
    .error(function(res) {
      $scope.isRecipeFavourite();
    })
  }

  $scope.removeFromFavourites = function(id) {
    $http.post('/api/removeFavoriteRecipe', { token: $routeParams.authToken, recipeid: id })
    .success(function(res) {
      $scope.isRecipeFavourite();
    })
    .error(function(res) {
      $scope.isRecipeFavourite();
    })
  }

  $scope.showRecipePostPath = "/api/Recipe/" + $route.current.params.recipeId;
  $scope.recipe = {};
  $scope.isRecipeFavourite();

  $http.get($scope.showRecipePostPath)
  .success(function (data) {
    $scope.recipe = {
      name: data["name"],
      description: data["description"],
      preparation: data["preparation"],
      photo: data["photo"],
      video: data["video"],
      readyIn: data["readyIn"],
      difficulty: data["difficulty"],
      category: data["category"],
      ingredients: data["ingredients"],
      user: data["user"],
      id: $route.current.params.recipeId,
    }

    if($scope.recipe.photo == "" || $scope.recipe.photo == "undefined") {
      $scope.recipe.photo = 'img/nophoto.jpg';
    }
    if($scope.recipe.video != "" && $scope.recipe.video != null) {
      if($scope.recipe.video.search("youtube") != -1) {
        $scope.recipe.video = $scope.recipe.video.replace("watch?v=", "v/");
        $scope.video = $sce.trustAsHtml('<iframe width="560" height="315" src="' + $scope.recipe.video + '" frameborder="0" allowfullscreen></iframe>');
      }
    }

    if(parseInt($scope.recipe.difficulty) <= 2) {
      $scope.difficultyImg = "difficult_light";
    } else if(parseInt($scope.recipe.difficulty) > 2 && parseInt($scope.recipe.difficulty) <= 4) {
      $scope.difficultyImg = "difficult_medium";
    } else {
      $scope.difficultyImg = "difficult_hard";
    }

    if(parseInt($scope.recipe.readyIn) <= 15) {
      $scope.timeImg = "time_light";
    } else if(parseInt($scope.recipe.readyIn) > 15 && parseInt($scope.recipe.readyIn) <= 45) {
      $scope.timeImg = "time_medium";
    } else {
      $scope.timeImg = "time_hard";
    }

    $rootScope.meta.title = $rootScope.meta.title + " - " + $scope.recipe.name;

    var concat = "";
    for(item in $scope.recipe) {
      if($scope.recipe[item] == null || $scope.recipe[item] == "" || $scope.recipe[item] == "0") {
        //nop
      } else {
      concat += $scope.recipe[item];
      }
    }
    if(concat == "") {
      $scope.notificationService.Add("alert", "Nie znaleziono przepisu.");
    }
  })
  .error(function (data) {
    $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
  })
}]);
