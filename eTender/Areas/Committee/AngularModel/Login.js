/// <reference path="angular-min-1.4.8.js" />
var Model = angular.module("LoginModule", []);

Model.controller("LoginController", function ($scope,LService) {
    $scope.LoginDetails = {}

    $scope.Login = function () { 
        LService.LoginPost($scope.LoginDetails).then(function (d) {
         
            if (d.data.msg == "You have login successfully")
            {
                window.location.href = "/Committee/CDash/";
            }
            else {
                alert(d.data.msg);
            }
        }, function (ex) {
            alert(JSON.stringify(ex));
        });
    };
});


Model.factory("LService", function ($http) {
    var fac = {};
    fac.LoginPost = function (logindetails) {
        return $http.post("/Committee/Login/Login", logindetails);
        
    };
    return fac;

});