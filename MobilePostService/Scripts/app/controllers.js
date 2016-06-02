'use strict';

var parcelControllers = angular.module('parcelControllers', []);

parcelControllers.controller('ParcelListCtrl', ['$scope', 'Parcel',
    function ($scope, Parcel) {
        $scope.parcels = Parcel.query();
    }
]);

parcelControllers.controller('CreateParcelFormCtrl', ['$scope', '$window',
 'Parcel', function ($scope, $window, Parcel) {
     $scope.submit = function () {
         Parcel.save($scope.parcel, function () {
             $window.location.href = '#';
         });
     };
 }]);