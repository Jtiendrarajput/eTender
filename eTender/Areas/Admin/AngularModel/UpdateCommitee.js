/// <reference path="angular-min-1.4.8.js" />


var Model = angular.module("UCModel", []);

Model.controller("UCController", function ($scope,UCService) {
    $scope.ViewList = true;
    $scope.ViewAllDetails = false;
    $scope.TendorList = {};
    $scope.TendorViewDetails = {};
    $scope.CommiteeMember = {};
    $scope.UCView = false;
    $scope.UniqueID = 0;
    var oldcomlength = 0;
    var lengthCM = 0;
    $scope.CommiteeForTendor = { "TenderID": 0, "ComMemeberID": [] };


    UCService.GetTendorList().then(function (d) {
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

        $scope.UCView = false;
        $scope.ViewAllDetails = false;
    }, function () { });

    $scope.ViewDetails = function (item) {
        $scope.TendorViewDetails = item;
        $scope.UniqueID = item.ID;
        UCService.GetCommiteeMemberListofTendor(item.ID).then(function (d) {
            var rs = d.data;
            oldcomlength = rs.length;
            $scope.CommiteeDetails = rs;
            $scope.ViewAllDetails = true;
            $scope.ViewList = false;
        }, function () { });

    };


    $scope.Edit = function () {

        UCService.GetCommiteeMemberList().then(function (d) {
            var rs = d.data;
            $scope.CommiteeMember = rs;
            lengthCM = rs.length;
            for(var i=0;i<rs.length;i++)
            {
                for(var j=0;j<oldcomlength;j++)
                {
                    if($scope.CommiteeMember[i].ID == $scope.CommiteeDetails[j].ID)
                    {
                        $scope.CommiteeMember[i].chk = true;
                        break;
                    }
                    else {
                        $scope.CommiteeMember[i].chk = false;
                    }
                }
            }
            $scope.ViewAllDetails = false;
            $scope.ViewList = false;
            $scope.UCView = true;
        }, function (ex) { alert(JSON.stringify(ex)); });

        
    };
    
    $scope.UpdateCommitee = function () {
        $scope.CommiteeForTendor.TenderID = $scope.UniqueID;
        var i = 0;
        for (var j = 0; j < lengthCM; j++) {
            if ($scope.CommiteeMember[j].chk == true) {
                $scope.CommiteeForTendor.ComMemeberID[i] = $scope.CommiteeMember[j].ID;
                chkornot = true;
                i = i + 1;
            }
        }

        if (chkornot == true) {
            //alert(JSON.stringify($scope.CommiteeForTendor));
            UCService.UpdateCommiteeMemberList($scope.CommiteeForTendor).then(function (d) {
                if (d.data.msg = "success") {
                    alert("Commitee is Updated Successfully");
                    $scope.BackToList();
                }
            }, function (ex) { alert(JSON.stringify(ex)); });


        }
        else {
            alert('Please Select atleast one Member for Commitee');
        }

    };


    $scope.DeleteCommitee = function()
    {
        var r = confirm("Are you want to Delete Commitee");

        if (r == true) {
            UCService.DeleteCommiteeMemberList($scope.UniqueID).then(function (d) {
                if (d.data.msg == "success") {
                    alert("Commitee is Successfully Deleted");
                }
                else
                {
                    alert(d.data.msg);
                }
            }, function (ex) { alert(JSON.stringify(ex)); });
        }
    }

    $scope.BackToList = function () {
        $scope.ViewList = true;
        $scope.ViewAllDetails = false;
        $scope.UCView = false;
    };
    
    $scope.BackToUpdate = function () {
        $scope.ViewList = false;
        $scope.ViewAllDetails = true;
        $scope.UCView = false;
    };

    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }
});


Model.factory("UCService", function ($http) {

    var fac = {};

    fac.GetTendorList = function () {
        return $http.get("/Admin/CommiteeUpdate/GetAllTendor");
    };


    fac.GetCommiteeMemberListofTendor = function (TendorID) {
        return $http.get("/Admin/CommiteeUpdate/GetAllCommiteeMemberOfTendor?TendorID="+ TendorID);
    };

    fac.GetCommiteeMemberList = function () {
        return $http.get("/Admin/CommiteeMember/GetAllCommiteeMember");
    };

    fac.UpdateCommiteeMemberList = function (Commitee) {
        return $http.post("/Admin/CommiteeUpdate/UpdateCommitee", Commitee);
    };

    fac.DeleteCommiteeMemberList = function (TendorID) {
        return $http.post("/Admin/CommiteeUpdate/UpdateCommitee?TendorID="+ TendorID);
    };

    return fac;
});