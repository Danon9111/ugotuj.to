//LoginUserController - The controller gives the ability to login user through restAPI.

//https://docs.angularjs.org/api/ngCookies/service/$cookieStore
// Możliwości dodawanie parametrów do COOKIES dostępne dopiero od Angulara 1.4.x.

app.controller('loginUserController', ['$scope', '$http', 'MessagePrintService', '$rootScope', '$cookies',function($scope, $http, MessagePrintService, $rootScope, $cookies) { 

  $scope.loginPostPath = "http://46.175.46.220/api/" + "login?";
  $scope.msgObj = MessagePrintService.msg();
                                         
  $rootScope.meta.title = "Logowanie";
  
  $scope.loading = false;
  $scope.loadingState = "hide";
  
  $scope.loginForm = {};
  $scope.loginForm.login = "";
  $scope.loginForm.password = "";
  $rootScope.auth = $cookies.authToken;                                       
  $scope.msgObj.msgPrint("info", "");
                                         
  $rootScope.authVerify = function(data) {
    $cookies.authToken = data.toString();
    $rootScope.auth = $cookies.authToken;
  }
  
  $scope.loginForm.submitTheForm = function(item, event) {
    var dataObject = {
      login : $scope.loginForm.login,
      password : $scope.loginForm.password
    };
    
    //http://46.175.46.220/api/login?login=bpit2&password=qwerty
    $http.post($scope.loginPostPath, "login=" + encodeURIComponent(dataObject.login) +
                        "&password=" + encodeURIComponent(dataObject.password),
                        {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
    .success(function (data) {
      $scope.loginForm.login = "";
      $scope.loginForm.password = "";    

      if(data["token"] != null) {
        $scope.msgObj.msgPrint("success", "Zalogowano! Token w ciasteczkach");
        $scope.meta.activeLinks = $rootScope.meta.userLinks;
        $rootScope.authVerify(data["token"]);
        setTimeout(function () {
           window.location.href = $rootScope.basePath;
        }, 1000);
      } else {
        $scope.msgObj.msgPrint("alert", data["error"]);
        $rootScope.authVerify("");
      }
    })
    .error(function (data) {
      $scope.msgObj.msgPrint("alert", "Failed to send data to server. try again in a while.");
      $rootScope.authVerify("authToken", "");
    })    
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