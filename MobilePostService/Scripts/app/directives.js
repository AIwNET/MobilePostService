'use strict';

/* Directives */

var parcelDirectives = angular.module('parcelDirectives', []);

parcelDirectives.directive('ngRedirectTo', ['$window', function ($window) {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            element.on('click', function () {
                $window.location.href = attributes.ngRedirectTo;
            });
        }
    }
}]);