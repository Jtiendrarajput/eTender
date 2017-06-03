/// <reference path="angular-min-1.4.8.js" />
var loginformactive = true;
var OpeModel = angular.module("OpeModel", []);

OpeModel.controller("VendorController", function (DService, $scope, $location) {
    $scope.VendorDetails = {};
    $scope.showsignup = false;
    $scope.showmobverify = false;
    $scope.showlogin = true;
    $scope.Verify = {};

    $scope.LoginDetails = { UserName: "info@indiainteractive.net", PassWord: "Nalini@123" };
    var mobilenumber = "";

    $scope.SIGNUP = function () {

        var chk1 = CheckPWD();
        var chk2 = CheckConfirmPWD();
        if (chk1 == true && chk2 == true) {
            DService.Signup($scope.VendorDetails).then(function (d) {
                // alert(d.data.msg);
                if (d.data.msg == 'success') {
                    alert('You have registered successfully.. Please Verify your Mobile Number');
                    mobilenumber = d.data.MobileNo;
                    $scope.ShowMobVerify();
                }
                else {
                    alert(d.data.msg);
                }


            }, function (ex) {
                alert(JSON.stringify(ex));
            });

        }
        else {
            $scope.innerhtml = 'Password did not match';

        }
    };

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
        if (!(strongRegex.test($scope.VendorDetails.Password))) {
            $scope.fpwdclass = true;
            $scope.innerhtml = "Password must have atleast 6 chracter along with one Uppercase letter, one special character and one number";
            if (!($scope.VendorDetails.Password.length == 0)) {
                document.getElementById('pwd1').focus();
            }
            $scope.VendorDetails.Password = "";
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
        if (!($scope.VendorDetails.Password == $scope.VendorDetails.ConfirmPassword)) {
            $scope.fpwdclass = true;
            $scope.innerhtml = "Password and Confirm Password must be same";
            if (!($scope.VendorDetails.ConfirmPassword.length == 0)) {
                document.getElementById('pwd2').focus();
            }

            $scope.VendorDetails.ConfirmPassword = "";
            return false;
        }
        else {
            $scope.innerhtml = " ";
            $scope.fpwdclass = false;
            return true;
        }
    }

    $scope.ChangeBidder = function () {
        if ($scope.VendorDetails.BidderPreRegisteredWith == "MSME Registration") {
            $scope.displaybidderinfo = true;
        }
        else {
            $scope.displaybidderinfo = false;
        }

    }

    $scope.ChangeBidderType = function () {
        if ($scope.VendorDetails.BidderRegisteredType == "Individual") {
            $scope.disptype = true;
        }
        else {
            $scope.disptype = false;
        }
    }


    $scope.ShowMobVerify = function () {
        $scope.Verify.MobileNumber = mobilenumber;
        $scope.showmobverify = true;
        $scope.showlogin = false;
    }
    $scope.ShowLogin = function () {

        $scope.showmobverify = false;
        $scope.showlogin = true;
    }

    $scope.OTPVerification = function () {
        DService.MobVerify($scope.Verify.MobileNumber, $scope.Verify.OTPCode).then(function (d) {
            alert(d.data.msg);
            //$scope.ShowLogin();
            window.location.href = "/Home/Index";
        }, function (ex) {
            alert(ex.data.msg);
        });
    }

    $scope.Login = function () {
        DService.Login($scope.LoginDetails).then(function (d) {
            // alert(d.data.msg);

            if (d.data.msg == "success") {
                var url = d.data.url;
                //DService.GetSignCommand().then(function (dd) {
                //    var response = dd.data.resp;
                //    $.post("http://127.0.0.1:1620", { "response": response }, function (d) {

                //        var status = $(d).find("status").text();
                //        if (status == "failed") {
                //            var error = $(d).find("error").text();
                //            var error_code = $(d).find("error").attr('code');
                //            alert('Error code : ' + error_code + '<br/>Error Message : ' + error + '</div>');
                //        }
                //        else {

                //            var data = $(d).find("data").text();
                //            var filetype = $(d).find("attribute").text();
                window.location.href = url;

                //           // alert('text is successfully signed.');
                //        }


                //    }).fail(function (xhr, textStatus, errorThrown) {
                //        console.log(" Server response not received.");
                //        alert('Signing services  not running..');
                //    });
                //}, function (er) {
                //    JSON.stringify(er);
                //});

            }

            else if (d.data.msg == "Mobile Number is not Verified") {
                alert(d.data.msg);
                $scope.ShowMobVerify();
            }

            else {
                alert(d.data.msg);
            }

        }, function (ex) {


            alert(JSON.stringify(ex));

        });
    }
    $scope.togelclick = function () {

        // Switches the Icon
        $('.toggle').children('i').toggleClass('fa-pencil');
        // Switches the forms  
        $('.form').animate({
            height: "toggle",
            'padding-top': 'toggle',
            'padding-bottom': 'toggle',
            opacity: "toggle"
        }, "slow");
        loginformactive = loginformactive ? false : true;
        if (!loginformactive) {
            DService.GetCertExtCommand().then(function (d) {
                var response = d.data.resp;
                $.post("http://127.0.0.1:1620", { "response": response }, function (d) {


                    var status = $(d).find("status").text();
                    if (status == "failed") {
                        var error = $(d).find("error").text();
                        var error_code = $(d).find("error").attr('code');
                        alert('Error code : ' + error_code + '<br/>Error Message : ' + error + '</div>');
                    }
                    else {

                        var data = $(d).find("data").text();


                        DService.GetCertDetails(data).then(function (crt) {
                            var rs = crt.data.CertDetails;
                            $scope.VendorDetails.CompanyName = rs.Organization_O;
                            $scope.VendorDetails.NameOfPartners = rs.SubjectName_CN;
                            $scope.VendorDetails.Email = rs.Email_E;
                            alert(JSON.stringify(rs));
                        }, function () { });

                    }



                }).fail(function (xhr, textStatus, errorThrown) {
                    console.log(" Server response not received.");
                    $("#msgtext").html("<div class='danger'>Signing services  not running..</div>");
                });
            }, function (e) { });
        }
    };

    $scope.ResendOTP = function () {
        if ($scope.Verify.MobileNumber == "" || $scope.Verify.MobileNumber == undefined) {
            alert('First Enter your Mobile Number then click resend OTP');
        }
        else {

            DService.SendOTP($scope.Verify.MobileNumber).then(function (d) {

                alert(d.data.msg);

            }, function (er) {
                alert(er.data.msg);

            });
        }
    }

    $("#dobid").focus(function () {
        this.type = "date";
    });
    $("#dobid").focusout(function () {
        this.type = "text";
    });
});


OpeModel.factory("DService", function ($http) {
    var fac = {};
    fac.Signup = function (vendor) {
        return $http.post('/Home/Signup', vendor);
    };

    fac.MobVerify = function (Mobile, OTP) {
        return $http.post('/Home/VerifyMobile?MobileNumber=' + Mobile + '&&OTP=' + OTP);
    };

    fac.Login = function (LoginDetails) {
        return $http.post('/Home/Login', LoginDetails);
    };


    fac.SendOTP = function (mobile) {
        return $http.post('/Home/RegenerateOTP?mobile=' + mobile);
    };
    fac.GetSignCommand = function () {
        return $http.get('/DSCcommand/Signin');
    };
    fac.GetCertExtCommand = function () {
        return $http.get('/DSCcommand/CertExtCommanccd');
    };
    fac.GetCertDetails = function (bs) {
        return $http.get('/DSCcommand/GetCertificateDetails', { params: { baseString: bs } });
    };
    return fac;

});
