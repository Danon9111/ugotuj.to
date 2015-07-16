//changeUserPasswordController - The controller gives the ability to login user through restAPI.

app.controller('changeUserPasswordController', ['$scope', '$http', 'MessagePrintService', '$rootScope', '$cookies', function($scope, $http, MessagePrintService, $rootScope, $cookies) { 

  $scope.postPath = "http://46.175.46.220/api/" + "ChangePassword";
  $scope.msgObj = MessagePrintService.msg();
  $scope.changeUserPasswordForm = {};
  $scope.changeUserPasswordForm.oldPassword = "";
  $scope.changeUserPasswordForm.newPassword = "";
  $scope.changeUserPasswordForm.newPasswordVerify = "";
  $rootScope.auth = $cookies.authToken;                                       
  $scope.msgObj.msgPrint("info", "");

  $scope.loading = false;
  $scope.loadingState = "hide";
  
  $rootScope.authVerify = function(data) {
    $cookies.authToken = data.toString();
    $rootScope.auth = $cookies.authToken;
  }
  
  $scope.changeUserPasswordForm.submitTheForm = function(item, event) {
    var dataObject = {
      oldPassword : $scope.changeUserPasswordForm.oldPassword,
      newPassword : $scope.changeUserPasswordForm.newPassword,
      newPasswordVerify : $scope.changeUserPasswordForm.newPasswordVerify,
      token : $rootScope.auth
    };
    
    
    if(dataObject.oldPassword == "" || dataObject.newPassword == "" || dataObject.newPasswordVerify != dataObject.newPassword) {
      $scope.msgObj.msgPrint("alert", "Incorrect data.");
    } else if(dataObject.token == "") {
      $scope.msgObj.msgClass = "msg-alert";
      $scope.msgObj.msgContent += " There's no token! Are you logged in?";
    } else {
      $http.post($scope.postPath, "token=" + encodeURIComponent(dataObject.token) +
                          "&oldPassword=" + encodeURIComponent(dataObject.oldPassword) +
                          "&newPassword=" + encodeURIComponent(dataObject.newPassword),
                          {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
      .success(function (data) {
        $scope.changeUserPasswordForm.oldPassword = "";
        $scope.changeUserPasswordForm.newPassword = "";
        $scope.changeUserPasswordForm.newPasswordVerify = "";
        if(data.toString() === "Incorrect token or old password!" || data.toString() === "Incorect new password!") {
          $scope.msgObj.msgPrint("alert", data.toString());
        } else {
          $scope.msgObj.msgPrint("success", data.toString());
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
    
  }
}]);