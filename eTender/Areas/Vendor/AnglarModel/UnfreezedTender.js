/// <reference path="angular-min-1.4.8.js" />


var OpeModel = angular.module("OpeModel", []);
OpeModel.controller("UnfreezedController", function ($scope,$http,UService) {

   // alert('hi');
    UService.GetUnfreezedTender().then(function (d) {
        var rs = d.data;
        $scope.UnfreezedTenderDetails = rs;
        for (var i = 0; i < rs.length; i++) {
            $scope.UnfreezedTenderDetails[i].BidStartDate = get_date($scope.UnfreezedTenderDetails[i].BidStartDate);
            $scope.UnfreezedTenderDetails[i].FreezDate = get_date($scope.UnfreezedTenderDetails[i].FreezDate);
        }
    }, function () { });


    $scope.ContinueBid = function (TenderID, ID) {

        UService.CheckBid(ID).then(function (d) {
            if (d.data == 0) {
                window.location.href = "/Vendor/StartTenderBid/Index?TID=" + TenderID;
            }
            else {
                alert('Your Bid is Already Freezed for this Tendor');
            }
        }, function () { });

    }


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }
});

OpeModel.factory("UService", function ($http) {
    var fac = {};

    fac.GetUnfreezedTender = function () {
        return $http.get("/Vendor/UnfreezedTender/UnfreezedTender");
    };

    fac.CheckBid = function (TendorID) {
        return $http.post('/ViewTender/CheckBidding?ID=' + TendorID);
    };

    return fac;


});