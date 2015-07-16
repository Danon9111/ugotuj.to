// create our angular app and inject ui.bootstrap
var app = angular.module('app', ['ui.bootstrap', 'ngCookies', 'ngRoute']).factory();



app.factory("MessagePrintService", function() {

  var msg = {};
  msg.msgClass = "msg-info";
  msg.msgContent = "";
  msg.msgPrint = function(type, content){

    var options = {
      "info" : "msg-info",
      "success" : "msg-success",
      "alert" : "msg-alert"
      };
    msg.msgClass = options[type.toString()];
    msg.msgContent = content.toString();
  };
  return {
    msg: function() {
      return msg;
    }
  };
});

app.run(function($rootScope, $cookies) {
  $rootScope.basePath = "/Frontend/";
  $rootScope.meta = {};
  $rootScope.meta.title = "Strona główna";
  $rootScope.meta.activeLinks = {};
  $rootScope.meta.userLinks = {"Wylogowanie" : "logout", "Dodaj przepis" : "addnewrecipe", "Zmień hasło" : "changepassword"};
  $rootScope.meta.incognitoLinks = {"rejestracja" : "register", "logowanie" : "login"};
  
  if($cookies.authToken != "" && $cookies.authToken != null) {
    $rootScope.meta.activeLinks = $rootScope.meta.userLinks;
  } else {
    $rootScope.meta.activeLinks = $rootScope.meta.incognitoLinks;
  }  
});

app.config(function($routeProvider, $locationProvider) {
  $routeProvider
    .when('/register', {
      templateUrl : 'register.html',
      controller  : 'registerNewUserController'
    })

    .when('/login', {
      templateUrl : 'login.html',
      controller  : 'loginUserController'
    })

    .when('/logout', {
      templateUrl : 'logout.html',
      controller  : 'logoutUserController'
    })

    .when('/addnewrecipe', {
      templateUrl : 'addnewrecipe.html',
      controller  : 'addNewRecipeController'
    })
  
    .when('/showrecipe/:recipeId', {
      templateUrl : 'showrecipe.html',
      controller  : 'showRecipeController'
    })

    .when('/changepassword', {
      templateUrl : 'changepassword.html',
      controller  : 'changeUserPasswordController'
    });
  
    //$locationProvider.html5Mode(true);
 });