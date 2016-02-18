var app = angular.module('homeApplication', ['ngGrid']);

app.controller('homeController', function ($scope, $http) {
    $http.get('/property/read').success(function (data) {
        $scope.propertymanagament = data; 
        $scope.searchProperty = '';
    });

     $scope.someProp = 'abc',
     $scope.editProperty = function () {
         $http.post('/property/editproperty').success(function (data) {
             $scope.propertymanagament.push(data);
         });
     };

    $scope.gridOptions = {
        data: 'propertymanagament',
        columnDefs: [{ field: 'upi', displayName: 'UPI' },
        { field: 'area', displayName: 'Area' },
        { name: 'Modify',
        cellTemplate: '<button type="button" class="btn btn-primary" ng-click="editProperty()">Modify</button>',
          name: 'Delete',
          cellTemplate: '<button type="button" class="btn btn-primary" ng-click="editProperty()">Delete</button>'
        }
         ]
    }; 

    //Create New Property
    $scope.property = function () {
        $http.post('/property/create').success(function (data) {
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