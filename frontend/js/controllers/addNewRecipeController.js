//addNewRecipeController - The controller gives the ability to add new recipe by a user.

app.controller('addNewRecipeController',['$scope', '$http', 'Notifications', '$rootScope', '$cookies', 'validateAuthToken', function($scope, $http, Notifications, $rootScope, $cookies, validateAuthToken) {

  var bgImages = ['addRecipeImg_4', 'addRecipeImg_5'];
  var bgRandomImage = bgImages[Math.floor(Math.random() * bgImages.length)];
  $rootScope.bgImage = 'img/' + bgRandomImage + '.jpg';

  $rootScope.meta.title = "Dodaj przepis";

  $scope.notificationService = Notifications;

  validateAuthToken.success(function(res) {
    $scope.validateAuthToken = res;
  });

  if($scope.validateAuthToken == false) {
    $scope.notificationService.Add("alert", "Hmm, wygląda na to, że nie jesteś zalogowany...");

  } else {
    /*Ingredients dynamic fields --> */
    $scope.ingredients = [{id: '1', ingredient: ''}];

    $scope.addIngredientField = function() {
      var newItemNo = $scope.ingredients.length + 1;
      $scope.ingredients.push({
        'id': newItemNo
      });
    };

    function findIndexByKeyValue(obj, key, value) {
      for (var i = 0; i < obj.length; i++) {
          if (obj[i][key] == value) {
              return i;
          }
      }
      return null;
    }

    $scope.removeIngredientField = function(ingId) {
      if($scope.ingredients.length > 1) {
        $scope.ingredients.splice(findIndexByKeyValue($scope.ingredients, "id", ingId), 1);
      } else {
        $scope.ingredients[findIndexByKeyValue($scope.ingredients, "id", ingId)].ingredient = "";
      }
    };

    $scope.ingredientsToHtml = function() {
      var makeHtml = "";
      var checkEmpty = "";
      for (var ing in $scope.ingredients) {
        checkEmpty += $scope.ingredients[ing].ingredient;
        if(checkEmpty.replace(/ /g,'') != "" && checkEmpty.replace(/ /g,'') != "undefined") {
          makeHtml += "<li>" + $scope.ingredients[ing].ingredient + "</li>";
        }
      }
      if(checkEmpty.replace(/ /g,'') != "" && checkEmpty.replace(/ /g,'') != "undefined") {
        return makeHtml;
      } else {
        return "";
      }

    };
    /* <-- Ingredients dynamic fields*/

    $scope.addNewRecipeForm = {
      name: '', description: '', preparation: '', photo: '', video: '',
      readyIn: '', difficulty: '', category: '', ingredients: ''
    };

    $scope.addNewRecipeForm.difficulty = '3'; //default.

    $scope.addNewRecipeForm.submitTheForm = function(item, event) {
      var dataObject = {
        token: $cookies.authToken,
        name: $scope.addNewRecipeForm.name,
        description: $scope.addNewRecipeForm.description,
        preparation: $scope.addNewRecipeForm.preparation,
        photo: $scope.addNewRecipeForm.photo,
        video: $scope.addNewRecipeForm.video,
        readyIn: $scope.addNewRecipeForm.readyIn,
        difficulty: $scope.addNewRecipeForm.difficulty,
        category: $scope.addNewRecipeForm.category,
        ingredients: $scope.ingredientsToHtml(),
      };

      $http.post('/api/Add', dataObject)
      .success(function (data) {
        if(data.toString() === "Przepis został dodany!") {
          $scope.notificationService.Add("success", data.toString());
          $scope.addNewRecipeForm.name = "";
          $scope.addNewRecipeForm.description = "";
          $scope.addNewRecipeForm.preparation = "";
          $scope.addNewRecipeForm.photo = "";
          $scope.addNewRecipeForm.video = "";
          $scope.addNewRecipeForm.readyIn = "";
          $scope.addNewRecipeForm.difficulty = '3'; //default.
          $scope.addNewRecipeForm.category = "";
          $scope.addNewRecipeForm.ingredients = "";
          $scope.ingredients = [{id: '1', ingredient: ''}];
        } else {
          $scope.notificationService.Add("alert", data.toString());
        }
      })
      .error(function (data) {
        $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
      })
    }
  }
}]);
