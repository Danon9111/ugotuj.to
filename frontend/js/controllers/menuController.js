/*
menuController - The controller gives the ability to load links depending on the user login state.
*/

app.controller('menuController',['$scope', '$http', '$cookies', function($scope, $http, $cookies) {

  $http.post('/api/userprofile', { token: $cookies.authToken })
  .success(function(res) {
    if(res != null && res != "") {
      $scope.userName = res.login;
    }
  });

}]);
