/// <reference path="angular-min-1.4.8.js" />


var model = angular.module("ChangeModel", []);


model.controller("ChangePasswordController", function ($scope,$http) {
    $scope.changepassword = {};
    //call function for password validation
    $scope.PWDMATCH = function () {
        // alert('call password match');
        CheckPWD();

    };

    //call Function to match confirm password 
    $scope.CPMATCH = function () {
        CheckConfirmPWD();
    }
    function CheckPWD() {
        var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})");
        if (!(strongRegex.test($scope.changepassword.newpassword))) {
            $scope.fpwdclass = true;
            $scope.innerhtml = "Password must have atleast 6 chracter along with one Uppercase letter, one special character and one number";
            if (!($scope.changepassword.newpassword.length == 0)) {
                document.getElementById('pwd1').focus();
            }
            $scope.changepassword.newpassword = "";
            return false;
            //event.preventDefault();
        }
        else {
            $scope.innerhtml = " ";
            $scope.fpwdclass = false;
            return true;
        }
    }

    //match password and confirm password are saem or not   
    function CheckConfirmPWD() {
        if (!($scope.changepassword.newpassword == $scope.changepassword.confirmPassword)) {
            $scope.fpwdclass = true;
            $scope.innerhtml = "Password and Confirm Password must be same";
            if (!($scope.changepassword.confirmPassword.length == 0)) {
                document.getElementById('pwd2').focus();
            }

            $scope.changepassword.confirmPassword = "";
            return false;
        }
        else {
            $scope.innerhtml = " ";
            $scope.fpwdclass = false;
            return true;
        }
    }



    $scope.ChangePassword = function () {
        var chk1 = CheckPWD();
        var chk2 = CheckConfirmPWD();
        if (chk1 == true && chk2 == true) {
            $http({
                method: "POST",
                data: $scope.changepassword,
                url: '/VDash/ChangePassword'
            }).success(function (d) {
                if (d.msg == 'success') {
                    $scope.changepassword = {};
                    alert('Password has been changed');
                }
                else
                    alert(d.msg);
            }).error(function () {
            });
            //call method here
        }
        else {
            $scope.innerhtml = 'Password did not match';
        }
    }

});


model.factory("CPService", function ($http) {
    var fac = {};

    fac.ChangePassword = function () {
        return $http.post();
    };

    return fac;

});