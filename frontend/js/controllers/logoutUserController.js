//LogoutUserController - The controller gives the ability to logout by clearing cookies.

app.controller('logoutUserController', ['$scope', '$http', 'MessagePrintService', '$rootScope', '$cookies', function($scope, $http, MessagePrintService, $rootScope, $cookies) { 

  $scope.loginPostPath = "http://46.175.46.220/api/" + "logout?";
  $rootScope.auth = $cookies.authToken;
  $scope.msgObj = MessagePrintService.msg();  
  
  $scope.meta.title = "Wyloguj";
  
  $rootScope.authVerify = function(data) {
    $cookies.authToken = data.toString();
    $rootScope.auth = $cookies.authToken;
  }
  
  $http.post($scope.loginPostPath, "token=" + encodeURIComponent($cookies.authToken),
                        {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
    .success(function (data) {
      $rootScope.authVerify("");
      $scope.msgObj.msgPrint("success", "wylogowano!");
      $scope.meta.activeLinks = $rootScope.meta.incognitoLinks;
      setTimeout(function () {
         window.location.href = $rootScope.basePath;
      }, 1000);
    })
    .error(function (data) {
      $scope.msgObj.msgPrint("alert", "Failed to send data to server. try again in a while.");
    })
  }

]);