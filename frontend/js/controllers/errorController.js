app.controller('errorController', ['$scope', '$rootScope', function($scope, $rootScope) {
  $rootScope.bgImage = "";
  $rootScope.meta.title = "Błąd!";
    setTimeout(function () {
       window.location.href = $rootScope.basePath;
    }, 1000);
}]);
