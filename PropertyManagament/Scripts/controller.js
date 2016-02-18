var app = angular.module('homeApplication', ['ngGrid']);

app.controller('homeController', function ($scope, $http) {
    $http.get('/service/propertymanagament').success(function (data) {
        $scope.propertymanagament = data; 
        $scope.searchProperty = '';
    });

     $scope.someProp = 'abc',
     $scope.editProperty = function () {
         $http.post('/service/editproperty').success(function (data) {
             $scope.propertymanagament.push(data);
         });
     };

    $scope.gridOptions = {
        data: 'propertymanagament',
        columnDefs: [{ field: 'upi', displayName: 'UPI' },
        { field: 'area', displayName: 'Area' },
        { name: 'Modify',
        cellTemplate: '<button class="btn primary" ng-click="editProperty()">Modify</button>'
        }
         ]
    }; 

    //Create New Property
    $scope.property = function () {
        $http.post('/service/property').success(function (data) {         
            $scope.propertymanagament.push(data);            
        });    
    }

    $scope.editingData = {};

    $scope.modify = function (property) {
        $scope.editingData[property.id] = true;
    };

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