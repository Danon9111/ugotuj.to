/*
error404 directive handles the error message view.
*/

app.directive('error404', function() {
  return {
    restrict: 'E',
    templateUrl: 'js/directives/error404.html'
  }
});
