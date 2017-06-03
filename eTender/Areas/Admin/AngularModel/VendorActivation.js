/// <reference path="angular-min-1.4.8.js" />


var Model = angular.module("VAModel", []);

Model.controller("VAController", function ($scope,VAService) {
    $scope.VendorDetails = {};
    $scope.ViewList = true;

    $scope.GetNonActivateFunc = function () {
        VAService.GetNonActivatedVendor().then(function (d) {
            var rs = d.data;
            $scope.VendorDetails = rs;
            for (var i = 0; i < rs.length; i++) {
                $scope.VendorDetails[i].DOB = new Date(get_date($scope.VendorDetails[i].DOB));
                $scope.VendorDetails[i].RegistrationDate = new Date(get_date($scope.VendorDetails[i].RegistrationDate));
            }

        }, function (ex) {
            alert(JSON.stringify(ex));
        });
    }

    $scope.GetNonActivateFunc();

    $scope.ViewDetails = function (item) {
        $scope.VendorData = item;
        $scope.ViewList = false;
    };


    $scope.ApproveVendorFunc = function () {
        VAService.ActivateVendor($scope.VendorData.userID).then(function (d) {
            if(d.data.Data == true)
            {
                alert('Vendor has Approved');
                $scope.GetNonActivateFunc();
                $scope.ViewList = true;
            }
            else
            {
                alert('No Record Found!! Or some Error Occurs');
                $scope.ViewList = true;
            }
        }, function (ex) {
            alert(JSON.stringify(ex));
        });

    };

    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

});

Model.factory("VAService", function ($http) {
    var fac = {};

    fac.GetNonActivatedVendor = function () {
        return $http.get("/Admin/VendorActivation/GetAllNonActivatedVendor");

    };

    fac.ActivateVendor = function (ID) {
        return $http.post("/Admin/VendorActivation/ApproveVendor?ID=" + ID);

    };

    return fac;

});