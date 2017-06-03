/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("UTModel", []);

Model.controller("UTController", function (UTService, $scope, $http) {
   // alert('hi');
    $scope.TendorList = {};
    $scope.ShowTendorList = true;
    $scope.TendorUniqueID = "";
    UTService.GetAllUnfreezedTendor().then(function (d) {
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
    }, function (ex) {
        alert(JSON.stringify(ex));
    });


    $scope.ViewBidders = function (ID,TendorID) {
        $scope.TendorUniqueID = TendorID;
        UTService.GetAllBidders(ID).then(function (d) {
            $scope.BiddersList = d.data;
            $scope.ShowTendorList = false;
        }, function () { });
    };


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }


});

Model.factory("UTService", function ($http) {
    var fac = {};

    fac.GetAllUnfreezedTendor = function () {
        return $http.get("/Committee/AllUnfreezedTenders/UnfreezedTenderslist");
    };

    fac.GetAllBidders = function (TendorID) {
        return $http.get("/Committee/AllUnfreezedTenders/AllUnfreezedTendersBidders?TenderId=" + TendorID);
    };
    return fac;
});