/*
LoginUserController - The controller gives the ability to login user through restAPI.
*/

app.controller('loginUserController', ['$scope', '$http', 'Notifications', '$rootScope', '$cookies', '$timeout',function($scope, $http, Notifications, $rootScope, $cookies, $timeout) {

  $rootScope.meta.title = "Logowanie";
  $rootScope.bgImage = "img/loginBg_3.jpg";

  $scope.notificationService = Notifications;

  $scope.loginForm = {
    login: "",
    password: ""
  };

  $scope.loginForm.submitTheForm = function(item, event) {
    var dataObject = {
      login : $scope.loginForm.login,
      password : $scope.loginForm.password
    };

    $scope.loading = true; //Start loading wheel animation.

    $http.post('/api/login?', "login=" + encodeURIComponent(dataObject.login) +
                        "&password=" + encodeURIComponent(dataObject.password),
                        {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
    .success(function (data) {
      if(data["token"] != null) {
        $scope.notificationService.Add("success", "Zalogowano!");
        $scope.meta.activeLinks = $rootScope.meta.userLinks;
        $cookies.authToken = data["token"];
        setTimeout(function () {
           window.location.href = $rootScope.basePath;
        }, 1000);
      } else {
        $scope.notificationService.Add("alert", data["error"]);
        $cookies.authToken = "";
      }
    })
    .error(function (data) {
      $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
      $cookies.authToken = "";
    })

    .finally(function() {
      $scope.loading = false;
    });
  }

}]);
