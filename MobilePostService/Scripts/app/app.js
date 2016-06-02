'use strict';

/* Modules */

var app = angular.module('mobilePost', ['ngRoute', 'parcelControllers', 'parcelServices', 'parcelDirectives']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/new', {
            templateUrl: '/Partials/parcel-form.html',
            controller: 'CreateParcelFormCtrl'
        })
        .otherwise({
	        templateUrl: '/Partials/parcel-list.html',
	        controller: 'ParcelListCtrl'
        });
}]);