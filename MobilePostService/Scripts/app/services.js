'use strict'

var parcelServices = angular.module('parcelServices', ['ngResource']);

parcelServices.factory('Parcel', ['$resource',
    function($resource) {
        return $resource('/api/parcelapi/:parcelId', {}, {
            query: { method: 'GET', isArray: true, headers: { 'Accept': 'application/json' } },
            save: { method: 'POST', headers: { 'Accept': 'application/json' } }
        });
}]);