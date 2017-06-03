/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("CatModel", []);

Model.controller("CatController", function (DService, $http, $scope) {

    $scope.Action = "ADD";
    //alert('hi');
    $scope.CatDetails = {};



    $scope.GetListCat = function () {
        DService.GetCategoryList().then(function (d) {
            $scope.CatList = d.data;
        }, function () { });
    };
    $scope.GetListCat();

    $scope.ActionCat = function () {
        if ($scope.Action == "ADD") {
            DService.AddCategory($scope.CatDetails).then(function (d) {
                if (d.data.msg == "success") {
                    if (d.data.ID == 0) {
                        alert("Category Already Exists");
                    }
                    else {
                        alert("Category is added successfully");
                        $scope.GetListCat();
                    }
                    $scope.CatDetails = {};
                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });
        }
        else if ($scope.Action == "Update") {
            DService.UpdateCategory($scope.CatDetails).then(function (d) {
                if (d.data.msg == "success") {
                    alert("Category is successfully Updated");
                    $scope.GetListCat();
                    $scope.Action = "ADD";
                    $scope.CatDetails = {};
                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });
        }
    };

    $scope.Edit = function (item) {
        var itemedit = item;
        $scope.CatDetails.CategoryName = itemedit.CategoryName;
        $scope.CatDetails.CatCode = itemedit.CatCode;
        $scope.CatDetails.ID = itemedit.ID;
        $scope.Action = "Update";
    };

    $scope.Cancel = function () {
        $scope.CatDetails = {};
        $scope.Action = "ADD";
    };
    $scope.Delete = function (ID) {
        var r = confirm("Are you want to Delete Category");

        if (r == true) {
            DService.DeleteCategory(ID).then(function () {
                if (d.data.msg == "success") {
                    alert("Category is successfully Deleted ");
                    $scope.GetListCat();
                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });

        }

    };


});

Model.factory("DService", function ($http) {
    var fac = {};


    fac.GetCategoryList = function () {
        return $http.get("/Admin/Category/GetAllCategory");
    };

    fac.AddCategory = function (Dept) {
        return $http.post("/Admin/Category/ADDCategory", Dept);
    };

    fac.UpdateCategory = function (Dept) {
        return $http.post("/Admin/Category/UpdateCategory", Dept);
    };

    fac.DeleteCategory = function (ID) {
        return $http.post("/Admin/Category/DeleteCategory/?CatID=" + ID);
    };

    return fac;
});