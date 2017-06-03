/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("AllottedModel", []);

Model.controller("AllottedController", function ($scope,$http,ATService) {
    $scope.TendorList = {};

    ATService.GetAllottedTendor().then(function (d) {

        var rs = d.data;
        $scope.TendorList = rs;

        for (var i = 0; i < rs.length; i++) {
            $scope.TendorList[i].ActiveDate = new Date(get_date($scope.TendorList[i].ActiveDate));
            $scope.TendorList[i].BidStartDate = new Date(get_date($scope.TendorList[i].BidStartDate));
            $scope.TendorList[i].FreezeDate = new Date(get_date($scope.TendorList[i].FreezeDate));
            $scope.TendorList[i].DownloadStartDate = new Date(get_date($scope.TendorList[i].DownloadStartDate));
            $scope.TendorList[i].DownloadEndDate = new Date(get_date($scope.TendorList[i].DownloadEndDate));
            $scope.TendorList[i].TechBidOpenDate = new Date(get_date($scope.TendorList[i].TechBidOpenDate));
            $scope.TendorList[i].FinancialBidOpenDate = new Date(get_date($scope.TendorList[i].FinancialBidOpenDate));
            $scope.TendorList[i].ClarificationStartDate = new Date(get_date($scope.TendorList[i].ClarificationStartDate));
            $scope.TendorList[i].ClarificationEndDate = new Date(get_date($scope.TendorList[i].ClarificationEndDate));
            $scope.TendorList[i].PublishDate = new Date(get_date($scope.TendorList[i].PublishDate));
        }
    }, function (ex) {alert(JSON.stringify(ex)); });


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

});

Model.factory("ATService", function ($http) {

    var fac = {};
    fac.GetAllottedTendor = function () {
        return $http.get("/Committee/All_AllotedTenders/AllotedTenders");
    };
    return fac;

});
