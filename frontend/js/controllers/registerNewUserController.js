//RegisterNewUserController - The controller gives the ability to register new user through restAPI.

app.controller('registerNewUserController', ['$scope', '$http', 'MessagePrintService', '$cookies', '$rootScope', function($scope, $http, MessagePrintService, $cookies, $rootScope) { 

  $scope.registerPostPath = "http://46.175.46.220/api/registration";
  $scope.msgObj = MessagePrintService.msg();
  
  $rootScope.meta.title = "Rejestracja";
  
  $scope.loading = false;
  $scope.loadingState = "hide";
  
  $scope.registerForm = {};
  $scope.registerForm.login = "";
  $scope.registerForm.email = "";
  $scope.registerForm.password = "";
  $scope.registerForm.passwordVerify = "";
  
  $scope.registerForm.submitTheForm = function(item, event) {
    var dataObject = {
      email : $scope.registerForm.email,
      login : $scope.registerForm.login,
      password : $scope.registerForm.password,
      passwordVerify : $scope.registerForm.passwordVerify
    };
    
    if(dataObject.email == "" || dataObject.login == "" || dataObject.password == "" || dataObject.passwordVerify == "") {
      $scope.msgObj.msgPrint("alert", "Niepoprawne dane");
    } else {
      if(dataObject.password.toString() === dataObject.passwordVerify.toString()) {
        $http.post($scope.registerPostPath, "email=" + encodeURIComponent(dataObject.email) +
                            "&login=" + encodeURIComponent(dataObject.login) +
                            "&password=" + encodeURIComponent(dataObject.password),
                            {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
        .success(function (data) {
          $scope.registerForm.login = "";
          $scope.registerForm.email = "";
          $scope.registerForm.password = "";
          $scope.registerForm.passwordVerify = "";
          if(data.toString() === "Konto użytkownika zostało dodane poprawnie!") {
            $scope.msgObj.msgPrint("success", data.toString());
            $rootScope.changeLinks();
          } else {
            $scope.msgObj.msgPrint("alert", data.toString());
          }
        })
        .error(function (data) {
          $scope.msgObj.msgPrint("alert", "Failed to send data to server. try again in a while.");

        })
      } else {
        $scope.msgObj.msgPrint("alert", "Hasła się nie zgadzają");
      }
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