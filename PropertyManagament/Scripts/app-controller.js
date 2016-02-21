var app = angular.module('homeApplication', ['ngGrid']);

app.controller('homeController',['$scope','sharedProperties','$http', function ($scope, sharedProperties,$http) {
    $http.get('/property/read').success(function (data) {
        $scope.propertyManagament = data; 
        $scope.searchProperty = '';
    });

    //$scope.$watch('property.mortgage', function () {      
    //    if ($scope.property.mortgage) {
    //        $scope.property.hasMortgage = true;
    //    }
    //    else {
    //        $scope.property.hasMortgage = false;
    //    }
    //});

    $http.get('/property/getEnum').success(function (data) {
        $scope.mannerofpermanentusage = data;
    });
        
     //$scope.editProperty = function () {
     //    $http.post('/property/editProperty').success(function (data) {
     //        $scope.propertymanagament.push(data);
     //    });
     //};

    //Create New Property
     $scope.newProperty = function () {
        $http.post('/property/createProperty').success(function (data) {
            $scope.propertyManagament.push(data);      
            $scope.property = data;
        });    
    }

    $scope.deleteProperty = function (property) {
        if (confirm('Are you sure?')) {
            $http.post('/property/deleteProperty', property).success(function (result) {
                if (result) {
                    $scope.propertyManagament.splice($scope.propertyManagament.indexOf(property), 1);
                }
            });
        }
    }

    $scope.updateProperty = function (property) {       
            $http.post('/property/updateProperty', property).success(function (result) {
                if (result) {
                    //  $scope.propertyManagament.splice($scope.propertyManagament.indexOf(property), 1);                   
                }
                $scope.show = false;
            });        
    }

    $scope.editProperty = function (property) {
        $scope.property = property;
        $scope.owner = null;
        $scope.addProperty();
        $scope.show = true;
        if ($scope.property.mortgage) {
            $scope.property.hasMortgage = true;
        }
        else {
            $scope.property.hasMortgage = false;
        }
    }
     
    //Create/Delete Mortgage
    $scope.manageMortgage = function (property) {
        if (!$scope.property.mortgage) {
            $http.post('/property/createMortgage', property).success(function (data) {
                $scope.property.mortgage = data;               
            });           
        }
        else {
            $scope.property.mortgage = null;
            $http.post('/property/updateProperty', property).success(function (result) {
                if (result) {
                }
            });
        }
    }

    //Create Owner
    $scope.createOwner = function (property) {       
        $http.post('/property/createOwner', property).success(function (owner) {
            if ($scope.property.owners) {
                $scope.property.owners.push(owner);
            }
            else {
                $scope.property.owners = [owner];
            }
            });      
    }

    //Delete Owner
    $scope.deleteOwner = function (property,owner) {  
        $scope.property.owners.splice($scope.property.owners.indexOf(property), 1);
            $http.post('/property/updateProperty', property).success(function (result) {
                if (result) {
                }
            });        
    }

    $scope.updateOwner = function (property, owner) {
        $scope.property.owners.pop(owner);
        if ($scope.property.owners) {
            $scope.property.owners.push(owner);
        }
        else {
            $scope.property.owners = [owner];
        }
        $http.post('/property/updateProperty', property).success(function (result) {
            if (result) {
                //  $scope.propertyManagament.splice($scope.propertyManagament.indexOf(property), 1);                   
            }
            $scope.showOnwerDetails = false;
        });
    }


   $scope.editOwner = function (owner) {
       $scope.owner = owner;
       $scope.addOwner();
       $scope.showOnwerDetails = true;
        //if ($scope.property.mortgage) {
        //    $scope.property.hasMortgage = true;
        //}
        //else {
        //    $scope.property.hasMortgage = false;
        //}
    }
    
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: true
    };
    $scope.totalServerItems = 0;
    $scope.pagingOptions = {
        pageSizes: [5, 10, 20],
        pageSize: 5,
        currentPage: 1
    };

    $scope.setPagingData = function (data, page, pageSize) {
        var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        $scope.propertyManagament = pagedData;
        $scope.totalServerItems = data.length;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                $http.get('/property/read').success(function (data) {
                    data = data.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    $scope.setPagingData(data, page, pageSize);
                });
            } else {
                $http.get('/property/read').success(function (largeLoad) {
                    $scope.setPagingData(largeLoad, page, pageSize);
                });
            }
        }, 100);
    };

    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true); 

    $scope.pmGridOptions = {
        data: 'propertyManagament',
        enablePaging: true,
        showFooter: true,
        enableRowSelection: false,
        totalServerItems: 'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions,
        columnDefs: [
        { field: 'upi', displayName: 'UPI' },
        { field: 'area', displayName: 'Area' },
        { field: 'manner_of_permanent_usage', displayName: 'Manner of Permanent Usage', width: 250 },
        {
            displayName: 'Actions',
            cellTemplate: '<button type="button" class="btn btn-primary" ng-model="show" ng-click="editProperty(row.entity)">Modify</button> \
                <button type="button" class="btn btn-primary" ng-click="deleteProperty(row.entity)">Delete</button>'
        }
        ]
    };

    $scope.getOwnersPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                $http.get("/property/getOnwesByProperty", { params: { property_id: sharedProperties.getProperty() } }).success(function (data) {
                    data = data.filter(function (item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    $scope.setOwnersPagingData(data, page, pageSize);
                });
            } else {
                $http.get("/property/getOnwesByProperty", { params: { property_id: sharedProperties.getProperty() } }).success(function (largeLoad) {
                    $scope.setOwnersPagingData(largeLoad, page, pageSize);
                });
            }
        }, 100);
    };


    $scope.setOwnersPagingData = function (data, page, pageSize) {
        var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
        $scope.property.owners = pagedData;
        $scope.totalServerItems = data.length;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.$watch('ownersFilterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getOwnersPagedDataAsync($scope.ownerPagingOptions.pageSize, $scope.ownerPagingOptions.currentPage, $scope.ownersFilterOptions.filterText);
        }
    }, true);

    $scope.ownersFilterOptions = {
        filterText: "",
        useExternalFilter: true,
    };

    $scope.oTotalServerItems = 0;
    $scope.ownerPagingOptions = {
        pageSizes: [5, 10, 20],
        pageSize: 5,
        currentPage: 1
    };

    $scope.ownersGridOptions = {
        data: 'property.owners',
        enablePaging: true,
        showFooter: true,
        enableRowSelection: false,
        totalServerItems: 'oTotalServerItems',
        pagingOptions: $scope.ownerPagingOptions,
        filterOptions: $scope.ownersFilterOptions,
        columnDefs: [
        { field: 'name', displayName: 'Name' },
        { field: 'last_name', displayName: 'Last Name' },
        { field: 'address', displayName: 'Address' },
        {
            displayName: 'Actions',
            cellTemplate: '<button type="button" class="btn btn-primary" ng-model="show" ng-click="editOwner(row.entity)">Modify</button> \
                <button type="button" class="btn btn-primary" ng-click="deleteOwner(row.entity)">Delete</button>'
        }
        ]
    };

    $scope.addProperty = function () {       
        sharedProperties.addProperty($scope.property);      
    };

    $scope.addOwner = function () {
        sharedProperties.addOwner($scope.owner);
    };
}])
    .service('sharedProperties', function () {
        var property = null;
        var owner = null;
        var flag = false;

        return {
            getProperty:function () {               
                return property;
            },

            addProperty: function (data) {
                property = data;
            },

            getOwner: function () {              
                return owner;
            },

            addOwner: function (data) {
                owner = data;
            }
        };
    });

 