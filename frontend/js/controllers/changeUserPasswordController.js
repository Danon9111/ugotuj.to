//changeUserPasswordController - The controller gives the ability change user password.

app.controller('changeUserPasswordController', ['$scope', '$http', 'Notifications', '$rootScope', '$cookies', 'validateAuthToken', function($scope, $http, Notifications, $rootScope, $cookies, validateAuthToken) {

  $rootScope.bgImage = "img/changePasswordImg.jpg";

  validateAuthToken.success(function(res) {
    $scope.validateAuthToken = res;
  });

  $scope.notificationService = Notifications;

  $scope.changeUserPasswordForm = {
    oldPassword: '',
    newPassword: '',
    newPasswordVerify: ''
  };

  $scope.changeUserPasswordForm.submitTheForm = function(item, event) {
    var dataObject = {
      oldPassword : $scope.changeUserPasswordForm.oldPassword,
      newPassword : $scope.changeUserPasswordForm.newPassword,
      newPasswordVerify : $scope.changeUserPasswordForm.newPasswordVerify,
      token : $cookies.authToken
    };


    if(dataObject.oldPassword == "" || dataObject.newPassword == "" || dataObject.newPasswordVerify != dataObject.newPassword) {
      $scope.notificationService.Add("alert", "Wprowadziłeś niepoprawne dane. Popraw je i spróbuj ponownie.");
    } else if($scope.validateAuthToken == false) {
      $scope.notificationService.Add("alert", "Hmm, wygląda na to, że nie jesteś zalogowany...");
    } else {
      $http.post('http://ugotuj.to.hostingasp.pl/api/ChangePassword', "token=" + encodeURIComponent(dataObject.token) +
                          "&oldPassword=" + encodeURIComponent(dataObject.oldPassword) +
                          "&newPassword=" + encodeURIComponent(dataObject.newPassword),
                          {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
      .success(function (data) {
        $scope.changeUserPasswordForm.oldPassword = "";
        $scope.changeUserPasswordForm.newPassword = "";
        $scope.changeUserPasswordForm.newPasswordVerify = "";
        if(data.toString() === "Nieprawidłowy token!" || data.toString() === "Nowe hasło jest nieprawidłowe!") {
          $scope.notificationService.Add("alert", data.toString());
        } else {
          $scope.notificationService.Add("success", data.toString());
        }
      })
      .error(function (data) {
        $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
        $cookies.authToken = "";
      })
    }
  }
}]);
