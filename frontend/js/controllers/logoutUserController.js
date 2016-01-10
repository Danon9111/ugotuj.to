/*
logoutUserController - The controller gives the ability to logout user through restAPI.
*/

app.controller('logoutUserController', ['$scope', '$http', 'Notifications', '$rootScope', '$cookies', function($scope, $http, Notifications, $rootScope, $cookies) {

  $scope.meta.title = "Wyloguj";
  $rootScope.bgImage = "";

  $scope.notificationService = Notifications;

  $http.post('http://ugotuj.to.hostingasp.pl/api/logout?', "token=" + encodeURIComponent($cookies.authToken),
                        {headers: {'Content-Type': 'application/x-www-form-urlencoded'}})
    .success(function (data) {
      $cookies.authToken = "";
      $scope.notificationService.Add("success", "Zostałeś wylogowany! Miłego dnia i wracaj szybko :).");
      $scope.meta.activeLinks = $rootScope.meta.incognitoLinks;
      setTimeout(function () {
         window.location.href = $rootScope.basePath;
      }, 1000);
    })
    .error(function (data) {
      $scope.notificationService.Add("alert", "Ups! :( Coś poszło nie tak. Spróbuj ponownie za chwilę.");
    })
  }

]);
