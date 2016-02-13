var app = angular.module('homeApplication', []);

app.controller('homeController', function ($scope, $http) {
    $http.get('/service/propertymanagament').success(function (data) {
        $scope.propertymanagament = data;
    });

    $scope.property = function () {
        $http.post('/service/property').success(function (data) {
            $scope.propertymanagament.push(data);
        });
    }

    //$scope.remove = function (dragon) {
    //    if (confirm('Are you sure?')) {
    //        $http.post('/service/remove', JSON.stringify(dragon)).success(function (result) {
    //            if (result) {
    //                $scope.dragons.splice($scope.dragons.indexOf(dragon), 1);
    //            }
    //        });
    //    }
    //}
});