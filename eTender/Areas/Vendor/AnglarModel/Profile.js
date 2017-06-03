/// <reference path="angular-min-1.4.8.js" />


var Model = angular.module("ProfileModel", []);

Model.controller("ProfileController", function (PService,$scope) {
    $scope.ViewProfile = true;
    $scope.VendorDetails = {};
    $scope.showmobverify = false;
    $scope.EditProfileView = false;
    $scope.EmailVerified = true;
    $scope.LoginEmailVerified = true;
    $scope.showEmailVerification = false;
    $scope.EVerify = {};
    var emailtype = 0 ;
        PService.GetVendorDetails().then(function (d) {
            var rs = d.data;
            $scope.VendorDetails = rs;
            $scope.VendorDetails.DOB = new Date(get_date($scope.VendorDetails.DOB));
            $scope.ChangeBidder();
            $scope.ChangeBidderType();
            if(rs.CEmailConfirmationStatus == 0)
            {
                $scope.EmailVerified = false;
            }
            else {
                $scope.EmailVerified = true;
            }

            if(rs.EmailConfirmationStatus == 0)
            {
                $scope.LoginEmailVerified = false;
            }
            else {
                $scope.LoginEmailVerified = true;
            }


            if ($scope.VendorDetails.BidderPreRegisteredWith == "MSME Registration")
            {
                $scope.BidderPreReg = true;
                if ($scope.VendorDetails.BidderRegisteredType == "Individual") {
                    $scope.catdisp = true;
                }
                else {
                    $scope.catdisp = false;
                }

            }
            else {
                $scope.BidderPreReg = false;
            }

        }, function (ex) { alert(JSON.stringify(ex)); });

    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

    $scope.MovetoEdit = function ()
    {
        $scope.ViewProfile = false;
        $scope.showmobverify = false;
        $scope.EditProfileView = true;
        $scope.showEmailVerification = false;
    }


    $scope.BacktoProfile = function()
    {
        $scope.ViewProfile = true;
        $scope.showmobverify = false;
        $scope.EditProfileView = false;
        $scope.showEmailVerification = false;
    }

    $scope.EditProfile = function () {
        PService.EditVendorDetails($scope.VendorDetails).then(function (d) {
            if (d.data.msg == "success") {
                alert("Profile is updated successfully");
                var rs = d.data.ID;
                if (rs.MobileConfirmationStatus == 0) {
                    $scope.showmobverify = true;
                    $scope.EditProfileView = false;
                    $scope.ViewProfile = false;
                    $scope.showEmailVerification = false;
                    $scope.Verify.MobileNumber = rs.MobileNumber;
                }
                else {
                    $scope.ViewProfile = true;
                    $scope.showmobverify = false;
                    $scope.EditProfileView = false;
                    $scope.showEmailVerification = false;
                    $scope.VendorDetails = rs;
                }
            }
            else {
                alert("Profile is Not Updated. There is some Problem.");
            }
        }, function (ex) { alert(JSON.stringify(ex)); });
    };

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

    $scope.OTPVerification = function () {
        PService.MobVerify($scope.Verify.MobileNumber, $scope.Verify.OTPCode).then(function (d) {
            if(d.data.msg == "success")
            {
                alert("Your Mobile Number is Verified Successfully");
                $scope.BacktoProfile();
            }
        }, function (ex) {
            alert(ex.data.msg);
        });
    }

    $scope.ResendOTP = function () {
        if ($scope.Verify.MobileNumber == "" || $scope.Verify.MobileNumber == undefined) {
            alert('First Enter your Mobile Number then click resend OTP');
        }
        else {

            PService.SendOTP($scope.Verify.MobileNumber).then(function (d) {
                alert(d.data.msg);
            }, function (er) {
                alert(er.data.msg);

            });
        }
    }

    function SendEmailOTP() {
        var email = "";
        if (emailtype == 0)
        {
            email = $scope.VendorDetails.ContactEmail;
        }
        else {
            email = $scope.VendorDetails.Email;
        }


        PService.SendOTPtoContactEmail(email,emailtype).then(function (d) {
            var rs = d.data;
            alert(rs);
            
        }, function (ex) { alert(JSON.stringify(ex)); });
    };


    $scope.MovetoEmailVerification = function () {
        $scope.EVerify.Email = $scope.VendorDetails.ContactEmail;
        $scope.showEmailVerification = true;
        $scope.showmobverify = false;
        $scope.EditProfileView = false;
        $scope.ViewProfile = false;
        emailtype = 0;
        SendEmailOTP();
    };

    $scope.MovetoLoginEmailVerification = function () {
        $scope.EVerify.Email = $scope.VendorDetails.Email;
        $scope.showEmailVerification = true;
        $scope.showmobverify = false;
        $scope.EditProfileView = false;
        $scope.ViewProfile = false;
        emailtype = 1;
        SendEmailOTP();
    };

    $scope.ResendOTPEmail = function () {
        SendEmailOTP();
    };

    $scope.EmailVerification = function () {
        PService.EmailVerify(emailtype, $scope.EVerify.Email, $scope.EVerify.OTPEmail).then(function (d) {

            if (d.data.msg == "success")
            {
                alert("Your Email is verified successfully");
                if (emailtype == 0) {
                    $scope.EmailVerified = true;
                }
                else {
                    $scope.LoginEmailVerified = true;
                }
                $scope.BacktoProfile();
                $scope.EVerify = {};
            }
            else {
                alert("There was some Problem !! Your email is not verified ");
            }

        }, function (ex) { alert(JSON.stringify(ex)); });

    };


});

Model.factory("PService", function ($http) {
    var fac = {};

    fac.GetVendorDetails = function () {
        return $http.get("/Vendor/Profile/VendorDetails");
    };

    fac.EditVendorDetails = function (vendor) {
        return $http.post("/Vendor/Profile/UpdateVendor", vendor);
    };

    fac.MobVerify = function (Mobile, OTP) {
        return $http.post('/Home/VerifyMobile?MobileNumber=' + Mobile + '&&OTP=' + OTP);
    };

    fac.SendOTP = function (mobile) {
        return $http.post('/Home/RegenerateOTP?mobile=' + mobile);
    };

    fac.SendOTPtoContactEmail = function (email,emailtype) {
        return $http.get('/Vendor/Profile/SendCEmailOTP?email=' + email + '&emailtype=' + emailtype);
    };

    fac.EmailVerify = function (emailtype,email,OTP) {
        return $http.post('/Vendor/Profile/VerifyEmail?emailtype=' + emailtype + '&email=' + email + '&OTP=' + OTP);
    };
    
    return fac;

});