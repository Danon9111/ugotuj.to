//showRecipeController - The controller gives the ability to load specific recipe by a user.

/*
info:
The $routeParams service is populated asynchronously. This means it will typically appear empty when first used in a controller.
Controllers used by a route, or within a template included by a route, will have immediate access to the fully-populated $routeParams because ng-view waits for the $routeChangeSuccess event before continuing. (It has to wait, since it needs the route information in order to decide which template/controller to even load.)

If you know your controller will be used inside of ng-view, you won't need to wait for the routing event. If you know your controller will not, you will. If you're not sure, you'll have to explicitly allow for both possibilities. Subscribing to $routeChangeSuccess will not be enough; you will only see the event if $routeParams wasn't already populated

What I discovered is that, $routeParams take some time to load in the Main Controller, it probably initiate the Main Controller first and then set $routeParams at the Child Controller.

$ROUTE $ROUTE $ROUTE $ROUTE
*/

app.controller('showRecipeController',['$scope', '$http', 'MessagePrintService', '$rootScope', '$routeParams', '$cookies', '$route',function($scope, $http, MessagePrintService, $rootScope, $cookies, $routeParams, $route) { 

  $rootScope.auth = $cookies.authToken;
  $scope.msgObj = MessagePrintService.msg();
  
  //console.log('routeParams:'+JSON.stringify($route.current.params.recipeId));
  $scope.showRecipePostPath = "http://46.175.46.220/api/Recipe/" + $route.current.params.recipeId;
  $scope.msgObj = MessagePrintService.msg();
                                         
  $rootScope.meta.title = "Przepis";
  
  $rootScope.auth = $cookies.authToken;                                       
  $scope.msgObj.msgPrint("info", "");
                                         
  $rootScope.authVerify = function(data) {
    $cookies.authToken = data.toString();
    $rootScope.auth = $cookies.authToken;
  }
  
  $scope.recipe = {};
  
  $http.get($scope.showRecipePostPath)
  .success(function (data) {
    $scope.recipe.name = data["name"];
    $scope.recipe.description = data["description"];
    $scope.recipe.preparation = data["preparation"];
    $scope.recipe.photo = data["photo"];
    $scope.recipe.video = data["video"];
    $scope.recipe.readyIn = data["readyIn"];
    $scope.recipe.difficulty = data["difficulty"];
    $scope.recipe.category = data["category"];
    $scope.recipe.ingredients = data["ingredients"];
    
    $scope.loadingState = "hide";
    
    var concat = "";
    for(item in $scope.recipe) {
      if($scope.recipe[item] == null || $scope.recipe[item] == "" || $scope.recipe[item] == "0") {
        //nop
      } else {
      concat += $scope.recipe[item];
      }
    }
    if(concat == "") {
      $scope.msgObj.msgPrint("alert", "Recipe not found!");
      setTimeout(function () {
         window.location.href = "index.html";
      }, 4000);    
    }
    //console.log(concat);
  })
  .error(function (data) {
    $scope.msgObj.msgPrint("alert", "Failed to send data to server. try again in a while.");
    $rootScope.authVerify("authToken", "");
  })    
  
}]);