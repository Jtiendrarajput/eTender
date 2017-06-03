/// <reference path="angular-min-1.4.8.js" />


var TenderID = 0;
var OpeModel = angular.module("OpeModel", []);
//alert('in javascript');
OpeModel.controller("TenderController", function ($scope, VTService) {
    //alert('hi');
    $scope.ViewTendorList = true;
    VTService.DispAllTender().then(function (d) {
        var rs = d.data;
        $scope.TenderDetails = rs;
        TenderID = $scope.TenderDetails.TenderID;
        for (var i = 0; i < rs.length; i++) {
            $scope.TenderDetails[i].BidStartDate = get_date($scope.TenderDetails[i].BidStartDate);
            $scope.TenderDetails[i].FreezeDate = get_date($scope.TenderDetails[i].FreezeDate);
            $scope.TenderDetails[i].DownloadStartDate = get_date($scope.TenderDetails[i].DownloadStartDate);
            $scope.TenderDetails[i].DownloadEndDate = get_date($scope.TenderDetails[i].DownloadEndDate);
            $scope.TenderDetails[i].TechBidOpenDate = get_date($scope.TenderDetails[i].TechBidOpenDate);
            $scope.TenderDetails[i].FinancialBidOpenDate = get_date($scope.TenderDetails[i].FinancialBidOpenDate);
            $scope.TenderDetails[i].ClarificationStartDate = get_date($scope.TenderDetails[i].ClarificationStartDate);
            $scope.TenderDetails[i].ClarificationEndDate = get_date($scope.TenderDetails[i].ClarificationEndDate);


        }
    }, function () { });

    $scope.ViewDetails = function (item) {
        $scope.TendorViewDetails = item;
        $scope.ViewTendorList = false;
    }


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

    $scope.StartBid = function (TenderID,ID) {
       
        VTService.CheckBid(ID).then(function (d) {
            if (d.data == 3)
            {
                alert("You can Start Bidding after " + $scope.TendorViewDetails.BidStartDate);
            }
            else if(d.data == 0)
            {
                window.location.href = "/Vendor/StartTenderBid/Index?TID=" + TenderID;
            }
            else {
                alert('Your Bid is Already Freezed for this Tendor');
            }
        }, function () { });
       
    }

});

OpeModel.factory("VTService", function ($http) {
    var fac = {};

    fac.DispAllTender = function () {
        return $http.get('/ViewTender/AllActiveTendors');
    };


    fac.CheckBid = function (TendorID) {
        return $http.post('/ViewTender/CheckBidding?ID=' + TendorID);
    };
    return fac;

});
