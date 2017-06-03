/// <reference path="angular-min-1.4.8.js" />


var model = angular.module("AppAddCommitteeMember", []);
model.controller("ControllerCommitteemember", function ($scope, $http, MService) {
    $scope.AllMember = null;
    $scope.Member = {};
    $scope.Action = "Save";
    $scope.ViewAll = "show";
    $scope.viewAdd = "hide";
    MService.getAllMember().then(function (d) {
        var rs = d.data;
        jQuery.each(rs, function (r) {
            this.visibleVerify = "hide";
            this.visibleVerifylnk = "show";
            this.OTP = '';
        });
        $scope.AllMember = rs;
    }, function (er) {
        alert(JSON.stringify(er));
    });
    $scope.submit = function () {
        if ($scope.Action == "Save") {
            $http({
                method: "POST",
                url: "/CommiteeMember/ADDCommiteeMember",
                data: $scope.Member
            }).success(function (d) {
                if (d.msg == "success") {
                    if (d.ID == 0) {
                        alert("Member is Already Exists");
                    }
                    else {
                        MService.getAllMember().then(function (d) {
                            var rs = d.data;
                            $scope.AllMember = rs;
                        }, function (er) {
                            alert(JSON.stringify(er));
                        });
                        alert('Member Add successfully');
                    }
                    $scope.Member = {};
                }
                else
                    alert(d.msg);
            }).error(function (er) {
                alert(JSON.stringify(er));
            });

        } else {

            $http({
                method: "POST",
                url: "/CommiteeMember/UpdateCommiteeMember",
                data: $scope.Member
            }).success(function (d) {
                if (d.msg == "success") {
                    MService.getAllMember().then(function (d) {
                        var rs = d.data;
                        $scope.AllMember = rs;
                    }, function (er) {
                        alert(JSON.stringify(er));
                    });
                    $scope.Member = {};
                    $scope.Action = "Save";
                    alert('Member update successfully');
                }
            }).error(function (er) {
                alert(JSON.stringify(er));
            });
        }
    };
    $scope.edit = function (item) {
        $scope.Member = item;
        $scope.ViewAll = "hide";
        $scope.viewAdd = "show";
        $scope.Action = "Update";
    };
    $scope.delete = function (item) {
        $http.post("/CommiteeMember/DeleteCommiteeMember", { CMemberID: item } ).success(function (d) {
            if (d.msg == "success") {
                MService.getAllMember().then(function (d) {
                    var rs = d.data;
                    $scope.AllMember = rs;
                }, function (er) {
                    alert(JSON.stringify(er));
                });
                alert('Member Has been Deleted');
            }
            else
                alert(d.msg);
        }).error(function (er) {
            alert(JSON.stringify(er));
        });
       
    };
    $scope.cancel = function () {
        $scope.Member = {};
        $scope.Action = "Save";
    };
    $scope.AddNew = function () {
        $scope.ViewAll = "hide";
        $scope.viewAdd = "show";
    };
    $scope.all = function () {
        $scope.ViewAll = "show";
        $scope.viewAdd = "hide";
    };

    $scope.verify = function (item) {
        if (item.MobileVerify == 0) {
            item.visibleVerify = "show";
            item.visibleVerifylnk = "hide";
        }
    };
    $scope.cancelverify = function (item) {
        item.visibleVerify = "hide";
        item.visibleVerifylnk = "show";
    };
    $scope.submitverify = function (item) {
        $http({
            method: "POST",
            data: item,
            url: '/CommiteeMember/VerifyMobile'
        }).success(function (d) {
            if (d.msg == "success") {
                item.MobileVerify = 1;
                item.visibleVerify = "hide";
                item.visibleVerifylnk = "show";
            }
            else
                alert(d.msg);
        }).error(function (er) { });
    };
    $scope.sendOTP = function (item) {
        $http({
            method: "POST",
            data: item,
            url: '/CommiteeMember/SentOTP'
        }).success(function (d) {
            if (d.msg == "success")
                alert('OTP has been sent successfylly');
            else
                alert(d.msg);
        }).error(function (er) {
            alert(JSON.stringify(er));
        });
    };
}).factory("MService", function ($http) {
    var fac = {};
    fac.getAllMember = function () {
        return $http.get('/CommiteeMember/GetAllCommiteeMember');
    };
    return fac;
});