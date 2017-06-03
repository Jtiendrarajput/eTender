/// <reference path="angular-min-1.4.8.js" />
/// <reference path="ViewTender.js" />

var model = angular.module("Mybid", []);
model.directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}])
model.controller("BidController", function ($scope, $http, BService) {
    $scope.xyz = "";
    $scope.ViewFeediv = true;
    $scope.ViewBidDocdiv = false;
    $scope.ViewAllDetails = false;
    $scope.ViewFinancialBidDocdiv = false;
    var tid = $('#tenderid').val();
    BService.StartBidding(tid).then(function (d) {
        var rs = d.data;
        $scope.BidDetails = rs.BidDetails;

        if ($scope.BidDetails.TechValidUpto != null && $scope.BidDetails.EMDValidUpto != null) {
            $scope.BidDetails.TechValidUpto = new Date(get_date($scope.BidDetails.TechValidUpto));
            $scope.BidDetails.EMDValidUpto = new Date(get_date($scope.BidDetails.EMDValidUpto));
        }
        $scope.TendorViewDetails = rs.TenderDetails;
        $scope.DepartmentName = rs.DepartmentName;
        $scope.CategoryName = rs.CategoryName;
        $scope.UserName = rs.UserName;
    }, function () { });
    var formdata = new FormData();

    function getNameFromPath(strFilepath) {
        var objRE = new RegExp(/([^\/\\]+)$/);
        var strName = objRE.exec(strFilepath);

        if (strName == null) {
            return null;
        }
        else {
            return strName[0];
        }
    }
    $scope.getTheFilestech = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append("tech", value);
            
        });
    };
    $scope.uploadtech = function () {

        var file = getNameFromPath($("#file1").val());
        if (file != null) {
            var extension = file.substr((file.lastIndexOf('.') + 1));
            if (extension == "pdf") {
                formdata.append("ID", $scope.BidDetails.ID);
                var request = {
                    method: 'POST',
                    url: '/DSCcommand/SignPDF/',
                    data: formdata,
                    headers: {
                        'Content-Type': undefined
                    }
                };
                $http(request)
                           .success(function (d) {
                              
                               if (d.msg == "success") {
                                   var response = d.resp;
                                   jQuery.post("http://127.0.0.1:1620", { "response": response }, function (d) {

                                       var status = $(d).find("status").text();
                                       if (status == "failed") {
                                           var error = $(d).find("error").text();
                                           var error_code = $(d).find("error").attr('code');
                                           alert('Error code : ' + error_code + '<br/>Error Message : ' + error + '</div>');
                                       }
                                       else {

                                           var data = $(d).find("data").text();
                                           //var filetype = $(d).find("attribute").text();
                                           var uploadfile = { val: $scope.BidDetails.ID, txt: data };

                                           $http({
                                               method: "POST",
                                               data: uploadfile,
                                               url: '/StartTenderBid/UploadTechDocumentWithSign'
                                           }).success(function (tfus) {
                                               if (tfus.msg == "success")
                                                   alert("File Upload success");
                                               else
                                                   alert('File Upload fail');
                                           }).error(function (er) {
                                               alert(JSON.stringify(er));
                                           });
                                           
                                           //alert('text is successfully signed.');
                                       }


                                   }).fail(function (xhr, textStatus, errorThrown) {
                                       console.log(" Server response not received.");
                                       alert('Signing services  not running..</div>');
                                   });
                                   
                               }
                               else {
                                   alert(d.msg);
                                   alert('File Upload Failed');
                               }
                           })
                           .error(function (er) {
                               alert(JSON.stringify(er));
                           });
                //var request = {
                //    method: 'POST',
                //    url: '/StartTenderBid/UploadTechDocument/',
                //    data: formdata,
                //    headers: {
                //        'Content-Type': undefined
                //    }
                //};
                //$http(request)
                //           .success(function (d) {
                //               if (d.msg == "success") {

                //                   alert("File Upload success");
                //               }
                //               else {
                //                   alert(d.msg);
                //                   alert('File Upload Failed');
                //               }
                //           })
                //           .error(function () {
                //           });
            }
            else {
                alert("Only *.pdf files are allowed.");
                formdata = new FormData();
            }
        }
        else {
            alert("Please select a file");
        }
    }

    


    $scope.getTheFilesFinancial = function ($files) {
        angular.forEach($files, function (value, key) {
           
            formdata.append("final", value);
        });
    };
    $scope.UploadFinancialDoc = function () {
        var file = getNameFromPath($("#file2").val());
        if (file != null) {
            var extension = file.substr((file.lastIndexOf('.') + 1));
            if (extension == "xls") {

        formdata.append("ID", $scope.BidDetails.ID);
        var request = {
            method: 'POST',
            url: '/StartTenderBid/UploadFinaDocument/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };
        $http(request)
                   .success(function (d) {
                       if (d.msg == "success") {

                           alert("File Upload success");
                       }
                       else {
                           alert(d.msg);
                           alert('File Upload Failed');
                       }
                   })
                   .error(function () {
                   });

            }
            else {
                alert("Only *.xls files are allowed.");
                formdata = new FormData();
            }
        }
        else {
            alert("Please select a file");
        }
    }

    $scope.Encrypt = function () {
        BService.UpdateBid($scope.BidDetails).then(function (d) {
            var rs1 =  d.data;
            $scope.BidDetails = rs1;
            $scope.ViewFeediv = false;
            $scope.ViewBidDocdiv = true;
            $scope.ViewAllDetails = false;
            $scope.ViewFinancialBidDocdiv = false;

        }, function () { });
    };

    $scope.ContinueTechnical = function () {
        if (formdata.has('tech'))
        {
            $scope.ViewFinancialBidDocdiv = true;
            $scope.ViewFeediv = false;
            $scope.ViewBidDocdiv = false;
            $scope.ViewAllDetails = false;
        }
        else{
            alert("First Upload Technical Document");
        }
    };

    $scope.BackFromTech = function () {
        $scope.ViewFeediv = true;
        $scope.ViewBidDocdiv = false;
        $scope.ViewAllDetails = false;
        $scope.ViewFinancialBidDocdiv = false;
    };


    $scope.BackFromFinancial = function () {
        $scope.ViewFeediv = false;
        $scope.ViewBidDocdiv = true;
        $scope.ViewAllDetails = false;
        $scope.ViewFinancialBidDocdiv = false;
    };
    $scope.Continue = function () {

        if (formdata.has('final')) {
            if ($scope.BidDetails.TechValidUpto != null && $scope.BidDetails.EMDValidUpto != null) {
                $scope.BidDetails.TechValidUpto = new Date(get_date($scope.BidDetails.TechValidUpto));
                $scope.BidDetails.EMDValidUpto = new Date(get_date($scope.BidDetails.EMDValidUpto));
            }

            BService.UpdateBid($scope.BidDetails).then(function (d) {
                var rs1 = d.data;
                $scope.BidDetails = rs1;
                if ($scope.BidDetails.TechValidUpto != null && $scope.BidDetails.EMDValidUpto != null) {
                    $scope.BidDetails.TechValidUpto = (get_date($scope.BidDetails.TechValidUpto));
                    $scope.BidDetails.EMDValidUpto = (get_date($scope.BidDetails.EMDValidUpto));
                }
                $scope.ViewFeediv = false;
                $scope.ViewBidDocdiv = false;
                $scope.ViewAllDetails = true;
                $scope.ViewFinancialBidDocdiv = false;
            }, function () { });
        }
        else {
            alert("First Upload Financial Document");
        }
    };

    $scope.BacktoForm = function () {
        $scope.ViewFeediv = true;
        $scope.ViewBidDocdiv = false;
        $scope.ViewAllDetails = false;
        $scope.ViewFinancialBidDocdiv = false;
    };


    $scope.Freezed = function () {
        BService.FreezeBid($scope.BidDetails).then(function (d) {
            var rs = d.data;
            if (rs.FreezeStatus == 1) {
                alert('Your Bid is Submitted Successfully');
                window.location.href = '/Vendor/ViewTender/Index';
            }
            else {
                alert('You Cannot freeze the bid. Last Date has Expired ');
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

model.factory("BService", function ($http) {
    var fac = {};

    fac.StartBidding = function (TenderID) {
        return $http.get('/Vendor/StartTenderBid/StartBidding', { params: { tenderID: TenderID } });
    };

    fac.UpdateBid = function (bid) {
        return $http.post('/Vendor/StartTenderBid/BidPayment', bid);
    };

    fac.FreezeBid = function (bid) {
        return $http.post('/Vendor/StartTenderBid/FreezeBid', bid);
    };

    return fac;
   
});
