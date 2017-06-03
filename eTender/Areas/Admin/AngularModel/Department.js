/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("DeptModel", []);

Model.controller("DeptController", function (DService, $http, $scope) {

    $scope.Action = "ADD";
    //alert('hi');
    $scope.DeptDetails = {};


    $scope.GetListDept = function () {
        DService.GetDepartmentList().then(function (d) {
            $scope.DeptList = d.data;
        }, function () { });
    };
    $scope.GetListDept();

    $scope.ActionDept = function () {
        if ($scope.Action == "ADD")
        {
            DService.AddDepartment($scope.DeptDetails).then(function (d) {
                if (d.data.msg == "success")
                {
                    if (d.data.ID == 0) {
                        alert("Department Already Exists");
                    }
                    else {
                        alert("Department is added successfully");
                        $scope.GetListDept();
                    }
                    $scope.DeptDetails = {};
                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });
        }
        else if ($scope.Action == "Update")
        {
            DService.UpdateDepartment($scope.DeptDetails).then(function (d) {
                if (d.data.msg == "success") {
                   
                        alert("Department is successfully Updated");
                        $scope.GetListDept();
                        $scope.Action = "ADD";
                    $scope.DeptDetails = {};
                }
                
            }, function (ex) {
                alert(JSON.stringify(ex));
            });
        }
    };

    $scope.Edit = function (item) {
        var itemedit = item;
        $scope.DeptDetails.ID = itemedit.ID;
        $scope.DeptDetails.DepartmentName = itemedit.DepartmentName;
        $scope.DeptDetails.DeptCode = itemedit.DeptCode;
        $scope.Action = "Update";
    };

    $scope.Cancel = function () {
        $scope.DeptDetails = {};
        $scope.Action = "ADD";
    };

    $scope.Delete = function (ID) {
        var r = confirm("Are you want to Delete Department");

        if (r == true) {
            DService.DeleteDepartment(ID).then(function () {
                if (d.data.msg == "success") {
                    alert("Department is successfully Deleted ");
                    $scope.GetListDept();
                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });

        }
      
    };


});

Model.factory("DService", function ($http) {
    var fac = {};
   

    fac.GetDepartmentList = function () {
        return $http.get("/Admin/Department/GetAllDepartment");
    };

    fac.AddDepartment = function (Dept) {
        return $http.post("/Admin/Department/ADDDept", Dept);
    };

    fac.UpdateDepartment = function (Dept) {
        return $http.post("/Admin/Department/UpdateDept", Dept);
    };

    fac.DeleteDepartment = function (ID) {
        return $http.post("/Admin/Department/DeleteDept/?DeptID="+ ID);
    };

    return fac;
});