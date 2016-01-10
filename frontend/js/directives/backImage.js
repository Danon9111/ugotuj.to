/*
backImage directive handles chnage of background image on the website.
*/

app.directive('backImage', function(){
    return function(scope, element, attrs){
        attrs.$observe('backImage', function(value) {
            element.css({
                'background-image': 'url(' + value +')',
                'background-size' : 'cover'
            });
        });
    };
});
