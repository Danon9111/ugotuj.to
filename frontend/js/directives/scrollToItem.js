/*
scrollToItem directive scrolls through content of page to the specifed elemnt in DOM.
*/

app.directive('scrollToItem', function() {
    return {
        restrict: 'A',
        scope: {
            scrollTo: "@"
        },
        link: function(scope, $elm,attr) {
          $elm.on('click', function() {
              $('html,body').animate({ scrollTop: $(scope.scrollTo).offset().top - 100 }, "slow");
          });
        }
    }});
