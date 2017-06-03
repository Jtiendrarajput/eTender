/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("TAModel", []);

Model.controller("TAController", function ($scope, TAService) {
    $scope.TendorList = {};
    $scope.TendorUniqueID =
    $scope.TendorID = 0;
    $scope.BiddersList = {};
    $scope.ShowTendorList = true;
    $scope.ViewBidderList = false;
    $scope.OpenTechnicalBidView = false;

    var ActionStatus = 0;
    var StatusType = "";
    var BidD = 0;
    var TendorID = 0;

    var OpenorUpdateStatus = "Open";

    TAService.GetAllTATendor().then(function (d) {
        var rs = d.data;
        $scope.TendorList = rs;
    }, function (ex) { alert(JSON.stringify(ex)); });
    

    $scope.ViewBidders = function(ID,TendorID)
    {
        $scope.TendorUniqueID = TendorID;
        $scope.TendorID = ID;

        TAService.GetBidders($scope.TendorID).then(function (d) {
            var rs = d.data;
            $scope.BiddersList = rs;
            for (var i = 0; i < rs.length; i++) {
                $scope.BiddersList[i].DOB = get_date($scope.BiddersList[i].DOB);
                $scope.BiddersList[i].BidSubmitDate = get_date($scope.BiddersList[i].BidSubmitDate);
                $scope.BiddersList[i].TechValidUpto = get_date($scope.BiddersList[i].TechValidUpto);
                $scope.BiddersList[i].EMDValidUpto = get_date($scope.BiddersList[i].EMDValidUpto);
                $scope.BiddersList[i].FreezeDate = get_date($scope.BiddersList[i].FreezeDate);
                $scope.BiddersList[i].LastActivityDate = get_date($scope.BiddersList[i].LastActivityDate);
                $scope.BiddersList[i].BidStatusID = ($scope.BiddersList[i].BidStatusID).toString();
            }
            $scope.ShowTendorList = false;
            $scope.ViewBidderList = true;
            $scope.OpenTechnicalBidView = false;

        }, function (ex) { alert(JSON.stringify(ex)); });
    }

    $scope.OpenFinancialBid = function(item)
    {
        ActionStatus = 2;
        StatusType = "Financial";
        BidD = item.BidID;
        TendorID = $scope.TendorID;
        $scope.BidDetails = item;

        TAService.SendOTPToAllCM(ActionStatus, StatusType, TendorID, BidD).then(function (d) {
            var rs = d.data;
            $scope.OTPDataCM = rs;
            if ($scope.OTPDataCM.Status == 1) {
                $scope.BidVisit = "OpenBid";
                document.getElementById('OpenPopup').click();
                $scope.BidDetails.BidStatusID = 2;
                OpenorUpdateStatus = "Open";
            }
            else {
                $scope.OpenTechnicalBidView = true;
                $scope.ViewBidderList = false;
                $scope.ShowTendorList = false;
                
            }
        
        },function(ex){alert(JSON.stringify(ex));});

    }


    $scope.BackfromBidder = function(){
        $scope.OpenTechnicalBidView = false;
        $scope.ViewBidderList = true;
        $scope.ShowTendorList = false;
    };


    $scope.VerifyOTPForAll = function () {

        TAService.VerifyOTPForAll2($scope.OTPDataCM).then(function (d) {
            var rs = d.data;
            if (rs == true) {
             
                document.getElementById('ClosePopup').click();

                $scope.OTPVerifiedSuccessFunc();
            }
            else {
                alert("OTP Verification failed");
            }

        }, function (ex) { alert(JSON.stringify(ex));});
    
    };

    $scope.UpdateStatus = function () {
        if ($scope.BidDetails.BidStatusID == 0 || $scope.BidDetails.BidStatusID == null) {
            alert("Please select Status")
        }
        else {

             ActionStatus = $scope.BidDetails.BidStatusID;
             StatusType = "Financial";
             BidD = $scope.BidDetails.BidID;
             TendorID = $scope.TendorID;

             TAService.SendOTPToAllCM(ActionStatus, StatusType, TendorID, BidD).then(function (d) {
                var rs = d.data;
                $scope.OTPDataCM = rs;
                if ($scope.OTPDataCM.Status == 1) {
                    document.getElementById('OpenPopup').click();
                    OpenorUpdateStatus = "Update";
                }
                else {
                    $scope.OpenTechnicalBidView = false;
                    $scope.ViewBidderList = true;
                    $scope.ShowTendorList = false;

                }
            }, function (ex) {
                alert(JSON.stringify(ex));
            });
        }

    };


    $scope.OTPVerifiedSuccessFunc = function () {
        TAService.AfterOTPVerfied(ActionStatus, StatusType, TendorID, BidD).then(function (d) {
            var rs = d.data;
            if (rs == true) {
                // alert("Status Was Changed Successfully");

                if (OpenorUpdateStatus == "Update") {
                    $scope.OpenTechnicalBidView = false;
                    $scope.ViewBidderList = false;
                    $scope.ShowTendorList = true;
                }
                else {
                    $scope.OpenTechnicalBidView = true;
                    $scope.ViewBidderList = false;
                    $scope.ShowTendorList = false;
                }
                
            }
            else {
                alert("There was some problem");
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
    };

});

Model.factory("TAService", function ($http) {
    var fac = {};

    fac.GetAllTATendor = function () {
        return $http.get("/Committee/TechnicalApprovedTendor/TechnicalApprovedTendor");
    };

    fac.GetBidders = function (ID) {
        return $http.get("/Committee/TechnicalApprovedTendor/TechnicalApprovedBidder?TendorID="+ ID);
    };

    fac.SendOTPToAllCM = function (ActionStatus, Status, TenderId, BiddingId) {
        return $http.post("/Committee/AllFreezedTender/SendOTPtoAllCM?ActionStatus=" + ActionStatus + "&&Status=" + Status + "&&TenderId=" + TenderId + "&&BiddingId=" + BiddingId);
    };

    
    fac.VerifyOTPForAll2 = function (TempOTPLst) {
        return $http.post("/Committee/AllFreezedTender/OTPVerifyForAll", TempOTPLst);
    }

    fac.AfterOTPVerfied = function (ActionStatus, Status, TenderId, BiddingId) {
        
        return $http.post("/Committee/AllFreezedTender/OTPVerifiedSuccessfully?ActionStatus=" + ActionStatus + "&&Status=" + Status + "&&TenderId=" + TenderId + "&&BiddingId=" + BiddingId);
    };

    return fac;
});