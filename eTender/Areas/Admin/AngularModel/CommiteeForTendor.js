/// <reference path="angular-min-1.4.8.js" />


var Model = angular.module("CFTModel", []);

Model.controller("CFTController", function (CFTService,$scope,$http) {

    $scope.TendorList = {};
    $scope.ViewList = true;
    $scope.CommiteeMember = {};
    $scope.TendorID = 0;
    $scope.UniqueID = 0;
    var lengthCM = 0;
    var chkornot = false;
    $scope.CommiteeForTendor = { "TenderID": 0, "ComMemeberID": [] };
    $scope.GetAllTendor = function () {
        CFTService.GetAllTendor().then(function (d) {
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
        }, function (er) { alert(JSON.stringify(er)); });
    }

    $scope.GetAllTendor();

    $scope.MovetoCommitee = function (ID,TendorID1) {
        $scope.TendorID = TendorID1;
        $scope.ViewList = false;
        $scope.UniqueID = ID;
        CFTService.GetAllCommiteeMember().then(function (d) {
            var rs = d.data;
            lengthCM = rs.length;
            $scope.CommiteeMember = rs;
            for (var i = 0; i < lengthCM; i++)
            {
                $scope.CommiteeMember[i].chk = false;
            }
           
        }, function (ex) { alert(JSON.stringify(ex)); });
    };

    
    $scope.CreateCommitee = function () {
        $scope.CommiteeForTendor.TenderID = $scope.UniqueID;
       
        for(var j=0;j<lengthCM;j++)
        {
            if ($scope.CommiteeMember[j].chk == true) {
                $scope.CommiteeForTendor.ComMemeberID[j] = $scope.CommiteeMember[j].ID;
                chkornot = true;
            }
        }

        if(chkornot == true)
        {
            //alert(JSON.stringify($scope.CommiteeForTendor));
            CFTService.AddCreateCommitee($scope.CommiteeForTendor).then(function (d) {
                if(d.data.msg="success")
                {
                    alert("Commitee is Created Successfully");
                    $scope.GetAllTendor();
                    $scope.ViewList = true;
                }
            }, function (ex) { alert(JSON.stringify(ex)); });

           
        }
        else {
            alert('Please Select atleast one Member for Commitee');
        }
    };


    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

    $scope.BackToList = function () {
        $scope.ViewList = true;
    };


});


Model.factory("CFTService", function ($http) {
    var fac = {};

    fac.GetAllTendor = function () {
        return $http.get("/Admin/CommiteeCreation/GetAllTendorForCommitteeCreation");
    };

    fac.GetAllCommiteeMember = function () {
        return $http.get("/Admin/CommiteeMember/GetAllCommiteeMember");
    };


    fac.AddCreateCommitee = function (Commitee) {
        return $http.post("/Admin/CommiteeCreation/ADDCommitee", Commitee);
    };


    return fac;


});