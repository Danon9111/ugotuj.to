/*
registerNewUserController - The controller gives the ability to register new user to the database.
*/

app.controller('registerNewUserController', ['$scope', '$http', 'Notifications', '$cookies', '$rootScope', function($scope, $http, Notifications, $cookies, $rootScope) {

  $rootScope.meta.title = "Rejestracja";
  $rootScope.bgImage = "img/registerImg.jpg";

  $scope.notificationService = Notifications;

  $scope.loading = false;

  $scope.registerForm = {};
  $scope.registerForm.login = "";
  $scope.registerForm.email = "";
  $scope.registerForm.password = "";
  $scope.registerForm.passwordVerify = "";
  $scope.registerForm.terms = false;

  $scope.doIfChecked = function(checkStatus) {
    $scope.registerForm.terms = checkStatus;
  }

  $scope.registerForm.submitTheForm = function(item, event) {
    var dataObject = {
      email : $scope.registerForm.email,
      login : $scope.registerForm.login,
      password : $scope.registerForm.password,
      passwordVerify : $scope.registerForm.passwordVerify
    };

    if(dataObject.email == "" || dataObject.login == "" || dataObject.password == "" || dataObject.passwordVerify == "") {
      $scope.notificationService.Add("alert", "Niepoprawne dane.");
    } else if($scope.registerForm.terms != true){
        $scope.notificationService.Add("alert", "Nie zaakceptowałeś regulaminu.");
    } else {
      if(dataObject.password.toString() === dataObject.passwordVerify.toString()) {
        $http.post('http://ugotuj.to.hostingasp.pl/api/registration', "email=" + encodeURIComponent(dataObject.email) +
                            "&login=" + encodeURIComponent(dataObject.login) +
                            "&password=" + encodeURIComponent(dataObject.password),
                            { headers: {'Content-Type': 'application/x-www-form-urlencoded'} })
        .success(function (data) {
          if(data.toString() === "Konto użytkownika zostało dodane poprawnie!") {
            $scope.registerForm.login = "";
            $scope.registerForm.email = "";
            $scope.registerForm.password = "";
            $scope.registerForm.passwordVerify = "";
            $scope.registerForm.terms = false;
            $scope.checkStatus = false;
            $scope.notificationService.Add("success", data.toString());
          } else {
            $scope.notificationService.Add("alert", data.toString());
          }
        })
        .error(function (data) {
          $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
        })
      } else {
        $scope.notificationService.Add("alert", "Hasła się nie zgadzają");
      }
    }
  }
}]);
