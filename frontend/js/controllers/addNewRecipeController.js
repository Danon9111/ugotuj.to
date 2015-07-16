//addNewRecipeController - The controller gives the ability to add new recipe by a user.

app.controller('addNewRecipeController',['$scope', '$http', 'MessagePrintService', '$rootScope', '$cookies', function($scope, $http, MessagePrintService, $rootScope, $cookies) { 

  $rootScope.auth = $cookies.authToken;
  $scope.msgObj = MessagePrintService.msg();
  
  $scope.addnewRecipePostPath = "http://46.175.46.220/api/" + "Add";
  $scope.msgObj = MessagePrintService.msg();
                                         
  $rootScope.meta.title = "Dodaj przepis";
  
  $scope.loading = false;
  $scope.loadingState = "hide";
  
  $scope.addNewRecipeForm = {};
  $scope.addNewRecipeForm.name = "";
  $scope.addNewRecipeForm.description = "";
  $scope.addNewRecipeForm.preparation = "";
  $scope.addNewRecipeForm.photo = "";
  $scope.addNewRecipeForm.video = "";
  $scope.addNewRecipeForm.readyIn = ""
  $scope.addNewRecipeForm.difficulty = ""; // 1-5.
  $scope.addNewRecipeForm.category = "";
  $scope.addNewRecipeForm.ingredients = "";
  
  $rootScope.auth = $cookies.authToken;                                       
  $scope.msgObj.msgPrint("info", "");
                                         
  $rootScope.authVerify = function(data) {
    $cookies.authToken = data.toString();
    $rootScope.auth = $cookies.authToken;
  }
  
  
  if($cookies.authToken == "" || $cookies.authToken == null) {
    $scope.msgObj.msgPrint("alert", "Tylko zalogowani użytkownicy mogą dodawć przepisy.");
    setTimeout(function () {
       window.location.href = "index.html#login";
    }, 4000);
  } else {

    $scope.addNewRecipeForm.submitTheForm = function(item, event) {
      var dataObject = {
        token: $rootScope.auth,
        name: $scope.addNewRecipeForm.name,
        description: $scope.addNewRecipeForm.description,
        preparation: $scope.addNewRecipeForm.preparation,
        photo: $scope.addNewRecipeForm.photo,
        video: $scope.addNewRecipeForm.video,
        readyIn: $scope.addNewRecipeForm.readyIn,
        difficulty: $scope.addNewRecipeForm.difficulty,
        category: $scope.addNewRecipeForm.category,
        ingredients: $scope.addNewRecipeForm.ingredients,
      };

      $http.post($scope.addnewRecipePostPath, dataObject)
      
      .success(function (data) {
        $scope.addNewRecipeForm.name = "";
        $scope.addNewRecipeForm.description = "";
        $scope.addNewRecipeForm.preparation = "";
        $scope.addNewRecipeForm.photo = "";
        $scope.addNewRecipeForm.video = "";
        $scope.addNewRecipeForm.readyIn = ""
        $scope.addNewRecipeForm.difficulty = ""; // 1-5.
        $scope.addNewRecipeForm.category = "";
        $scope.addNewRecipeForm.ingredients = "";

        if(data.toString() === "Recipe was added!") {
          $scope.msgObj.msgPrint("success", data.toString());
        } else {
          $scope.msgObj.msgPrint("alert", data.toString());
        }
        
      })
      .error(function (data) {
        $scope.msgObj.msgPrint("alert", "Failed to send data to server. try again in a while.");
      })
    }
  }
  
  $scope.loadingToggle = function() {
    if($scope.loading === true) {
      $scope.loading = false;
      $scope.loadingState = "hide";
    } else {
      $scope.loading = true;
      $scope.loadingState = "show";
    }
  };
  
}]);