/* Main app creation. */
var app = angular.module('app', ['ui.bootstrap', 'ngCookies', 'ngRoute', 'textAngular', 'ngAnimate', 'swxLocalStorage', 'ngSanitize']);


/* Notifications service that handles popup alerts. */
app.factory("Notifications", function() {

  var notifications = {
    all: [],
    Add: function(type, content) {
      while(notifications.all.length > 2) {
        notifications.all.splice(0, 1);
      }
      notifications.all.push({
        type: type,
        content: content
      });
    },
    Remove: function(self) {
      notifications.all.splice(notifications.all.indexOf(self), 1);
    }
  };

  return notifications;
});

/* Code that is running at start of the page. */
app.run(function($rootScope, $cookies, $cookieStore) {
  $rootScope.basePath = "/";
  $rootScope.meta = {};
  $rootScope.meta.title = "Strona główna";
  $rootScope.meta.activeLinks = {};

  //For menu purposes.
  if($cookies.authToken != null && $cookies.authToken != "") {
    $rootScope.loggedIn = true;
  } else {
    $rootScope.loggedIn = false;
  }
});

/* Service for validating if token which is in the cookies is in the database. */
app.factory('validateAuthToken', ['$http', '$cookies', '$rootScope', function($http, $cookies, $rootScope) {
  return $http.post('/api/auth', $cookies.authToken)

  .success(function(res) {
    if(res === false) {
      $cookies.authToken = "";
      $rootScope.loggedIn = false;
    }
    return res;
  })

  .error(function(res) {
    return res.toString();
  });
}]);

/* Filter for reversing order of a list. */
app.filter('reverse', function() {
  return function(items) {
    if(items != "" && items != null) {
      return items.slice().reverse();
    } else {
      return items;
    }
  };
});

/* Filter for removing html tags from string. */
app.filter('htmlToPlaintext', function() {
  return function(text) {
    return  text ? String(text).replace(/<[^>]+>/gm, '') : '';
  };
});

/* Filter for shortening string and adding '...' at the end of it. */
app.filter('shortenText', function() {
  return function(text) {
    return  String(text).length > 50 ? String(text).substring(0, 50) + '...' : text;
  };
});

/* Routing. */
app.config(function($routeProvider, $locationProvider, $httpProvider) {
  $routeProvider
    .when('/home', {
      templateUrl : 'home.html',
      title: 'Strona główna'
    })

    .when('/', {
      templateUrl : 'home.html',
      title: 'Strona główna'
    })

    .when('/recipe/search', {
      templateUrl : 'search.html',
      controller  : 'searchController',
      title: 'Wyszukaj przepis'
    })

    .when('/account/register', {
      templateUrl : 'register.html',
      controller  : 'registerNewUserController',
      title: 'Zarejestruj się'
    })

    .when('/account/login', {
      templateUrl : 'login.html',
      controller  : 'loginUserController',
      title: 'Zaloguj się'
    })

    .when('/account/logout', {
      templateUrl : 'logout.html',
      controller  : 'logoutUserController',
      title: 'Wyloguj się'
    })

    .when('/account/profile', {
      templateUrl : 'account.html',
      title: 'Mój profil'
    })

    .when('/recipe/add', {
      templateUrl : 'addnewrecipe.html',
      controller  : 'addNewRecipeController',
      title: 'Dodaj przepis'
    })

    .when('/recipe/:recipeId/', {
      templateUrl : 'showrecipe.html',
      title: 'Przepis'
    })

    .when('/account/changepassword', {
      templateUrl : 'changepassword.html',
      controller  : 'changeUserPasswordController',
      title: 'Zmień hasło'
    })

    .when('/403', {
      templateUrl : 'js/directives/error403.html',
      title: 'Błąd 403'
    })

    .otherwise({
      redirectTo : '/ohmy',
      templateUrl : 'js/directives/error404.html',
      title: 'Błąd 404 - Nie znaleziono strony'
    });

    $locationProvider.html5Mode(true);
 });
