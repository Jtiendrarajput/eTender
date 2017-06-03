/// <reference path="angular-min-1.4.8.js" />


var Model = angular.module("UCModel", []);
Model.controller("UCController", function ($http,$scope,UCService) {
    $scope.ViewList = true;
    $scope.UCView = false;



});

Model.factory("UCService", function ($http) {
    var fac = {};

    return fac;

});