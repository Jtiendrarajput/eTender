/// <reference path="angular-min-1.4.8.js" />

var model = angular.module("MyTender", []);

model.controller("TenderController", function ($scope, $http, TService) {

    TService.GetAllTenderHistory().then(function (d) {
        var rs = d.data;
        $scope.TenderDetails = rs;

        for (var i = 0; i < rs.length; i++) {
            $scope.TenderDetails[i].BidStartDate = get_date($scope.TenderDetails[i].BidStartDate);
            $scope.TenderDetails[i].FreezDate = get_date($scope.TenderDetails[i].FreezDate);
        }
    }, function () { });



    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }
});

model.factory("TService", function ($http) {

    var fac = {};

    fac.GetAllTenderHistory = function () {
        return $http.get("/Vendor/TenderHistory/TenderHistoryDetail");
    };

    return fac;
});