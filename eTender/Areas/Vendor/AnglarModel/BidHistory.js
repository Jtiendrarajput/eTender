/// <reference path="angular-min-1.4.8.js" />

var OpeModel = angular.module("OpeModel", []);

OpeModel.controller("BidHistoryController", function ($scope, BidService) {
    $scope.ViewBidList = true;
    $scope.AllBidDetailsBidders = {};
    BidService.GetBidHistory().then(function (d) {
        
        var rs = d.data;
        $scope.BidHistoryDetails = rs;
        for (var i = 0; i < rs.length; i++) {
            //alert($scope.BidHistoryDetails[i].FreezeDate);
            $scope.BidHistoryDetails[i].BidSubmitDate = get_date($scope.BidHistoryDetails[i].BidSubmitDate);

        }
    }, function () { });




    $scope.ViewDetails = function (ID) {
        // alert(ID);
        BidService.GetBidHistoryDetailsMember(ID).then(function (d) {
            var rs = d.data;
            $scope.AllBidDetails = rs;
            for (var i = 0; i < rs.length; i++) {
                $scope.AllBidDetails[i].BidStartDate = get_date($scope.AllBidDetails[i].BidStartDate);
                $scope.AllBidDetails[i].FreezeDate = get_date($scope.AllBidDetails[i].FreezeDate);
               
            }


            BidService.GerBidHistoryDetails(ID).then(function (d) {
                var rs1 = d.data;
                $scope.AllBidDetailsBidders = rs1;
                for (var j = 0; j < rs1.length; j++) {
                    $scope.AllBidDetailsBidders[j].BidSubmitDate = get_date($scope.AllBidDetailsBidders[j].BidSubmitDate);
                }
                $scope.ViewBidList = false;

            }, function (ex) { alert(JSON.stringify(ex)); });

        }, function (ex) { alert(JSON.stringify(ex)); });

      
    };


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }
});


OpeModel.factory("BidService", function ($http) {
    var fac = {};
    fac.GetBidHistory = function () {
        return $http.get("/Vendor/BidHistory/AllBidHistoy");
    };


    fac.GerBidHistoryDetails = function (TenderID)
    {
        return $http.get("/Vendor/BidHistory/BidHistoryDetail?TenderID="+ TenderID);
    };

    fac.GetBidHistoryDetailsMember = function (TenderID) {
        return $http.get("/Vendor/BidHistory/TendorMemberList?TenderID=" + TenderID);
    };
    return fac;

});