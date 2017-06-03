/// <reference path="angular-min-1.4.8.js" />

var Model = angular.module("TendorModule", []);
Model.directive('ngFiles', ['$parse', function ($parse) {

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
Model.controller("TendorController", function ($scope, TService,$http) { 
    $scope.Departments = {};
    $scope.Category = {};
    $scope.TendorList = {};
    $scope.TendorDetails = {};
    $scope.ViewList = true;
    $scope.Action = "Create Tendor";
    $scope.filename1 = "";
    $scope.filename2 = "";
    $scope.filename3 = "";
    $scope.SubmitTendor = false;
    $scope.CreateTendorForm = false;

    var Deptlen =0;
    var Catlen =0;
    var TendorID = 0;
    var formdata = new FormData();

    $scope.GetAllTendor = function () {
        TService.GetAllTendor().then(function (d) {
            var rs = d.data;
            $scope.TendorList = rs;

            for(var i=0;i<rs.length;i++)
            {
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

    TService.GetDepartment().then(function (d) {
        var rs = d.data;
        Deptlen = rs.length;
         var depts = [{ ID: '', DeptName: '--Select--' }];
         for (var i = 0; i < rs.length; i++) {
                depts.push({ ID: rs[i].ID, DeptName: rs[i].DepartmentName });
        }
            $scope.Departments = { lst: depts, selected: depts[0] };
        }, function (er) {
                 alert(JSON.stringify(er));
        });

    
    TService.GetCategory().then(function (d) {
        var rs = d.data;
        Catlen = rs.length;
        var cat = [{ ID: '', CatName: '--Select--' }];
        for (var i = 0; i < rs.length; i++) {
            cat.push({ ID: rs[i].ID, CatName: rs[i].CategoryName });
        }
        $scope.Category = { lst: cat, selected: cat[0] };
    }, function (er) {
        alert(JSON.stringify(er));
    });

    $scope.ADDNEW = function () {
        $scope.ViewList = false;
        $scope.SubmitTendor = false;
        $scope.CreateTendorForm = true;
    };

    function get_date(jsdate) {

        var jsonDate = jsdate;  // returns "/Date(1245398693390)/"; 
        var re = /-?\d+/;
        var m = re.exec(jsonDate);
        var d = new Date(parseInt(m[0]));

        return d;
    }

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
    
    $scope.CreateTendor = function () {

       
        if ($scope.DateVerification() == true) {

            if ($scope.Action == "Create Tendor") {

                if (formdata.has('tendornotice') && formdata.has('tendordoc') && formdata.has('boqfile')) {


                        $scope.TendorDetails.DepartmentID = $scope.Departments.selected.ID;
                        $scope.TendorDetails.CategoryID = $scope.Category.selected.ID;

                        TService.ADDCreateTendor($scope.TendorDetails).then(function (d) {
                            if (d.data.msg == "success") {
                                TendorID = d.data.ID;
                                $scope.uploadtech(TendorID);
                            }
                            else {
                                alert("There was some problem !!");
                            }

                        }, function (ex) { alert(JSON.stringify(ex)); });
                    
                }
                else {
                    alert("Please Upload All Files ");
                }
            }

            else if ($scope.Action == "Update") {
                $scope.TendorDetails.DepartmentID = $scope.Departments.selected.ID;
                $scope.TendorDetails.CategoryID = $scope.Category.selected.ID;

                TService.UpdateTendor($scope.TendorDetails).then(function (d) {
                    if (d.data.msg == "success") {
                        TendorID = d.data.ID;
                        $scope.uploadtech(TendorID);
                        
                    }
                }, function (ex) { alert(JSON.stringify(ex)); });

            }
        }
       
    };


    $scope.Edit = function (item) {
        TService.GetPermissionToUpdate(item.ID).then(function (d) {
            if (d.data.Status == 1)
            {
                $scope.TendorDetails = item;
                $scope.ViewList = false;
                $scope.SubmitTendor = false;
                $scope.CreateTendorForm = true;
               
                $scope.Action = "Update";
                for (var i = 0; i <= Deptlen; i++)
                {
                    if ($scope.TendorDetails.DepartmentID == $scope.Departments.lst[i].ID)
                    {
                        $scope.Departments.selected = $scope.Departments.lst[i];
                    }
                }

                for (var i = 0; i <= Catlen; i++)
                {
                    if ($scope.TendorDetails.CategoryID == $scope.Category.lst[i].ID) {
                        $scope.Category.selected = $scope.Category.lst[i];
                    }
                }

                $scope.filename1 = $scope.TendorDetails.TenderNoticePath;
                $scope.filename2 = $scope.TendorDetails.TenderDocPath;
                $scope.filename3 = $scope.TendorDetails.BOQFilePath;
                //$scope.Departments.selected.ID = $scope.TendorDetails.DepartmentID;
                //$scope.Category.selected.ID = $scope.TendorDetails.CategoryID;
                //$scope.Departments.selected.DeptName = $scope.TendorDetails.DeptName;
                //$scope.Category.selected.CatName = $scope.TendorDetails.CateName;
            }
            else {
                alert("This Tendor is now Active. You does not have permission to update it.");
            }
        }, function () { });
    };

    $scope.DateVerification = function () {
        if ($scope.TendorDetails.ActiveDate == null || $scope.TendorDetails.ActiveDate == undefined)
        {
            alert("First Select Active Date");
            $scope.TendorDetails.DownloadStartDate = null;
            return false;
        }
        else if ($scope.TendorDetails.ActiveDate > $scope.TendorDetails.DownloadStartDate)
            {
                alert("Please select Download start date greater than Active Date");
                $scope.TendorDetails.DownloadStartDate = null;
                return false;
            }
        else if ($scope.TendorDetails.DownloadStartDate == null || $scope.TendorDetails.DownloadStartDate == undefined) {
            alert("First Select Download Start Date");
            $scope.TendorDetails.DownloadEndDate = null;
            return false;
        }
        else if ($scope.TendorDetails.DownloadStartDate > $scope.TendorDetails.DownloadEndDate) {
            alert("Please select Download End Date greater than Download Start Date");
            $scope.TendorDetails.DownloadEndDate = null;
            return false;
        }
        else if ($scope.TendorDetails.DownloadEndDate == null || $scope.TendorDetails.DownloadEndDate == undefined) {
            alert("First Select Download End Date");
            $scope.TendorDetails.BidStartDate = null;
            return false;
        }
        else if ($scope.TendorDetails.DownloadEndDate > $scope.TendorDetails.BidStartDate) {
            alert("Please select Bid Start Date greater than Download End Date");
            $scope.TendorDetails.BidStartDate = null;
            return false;
        }
        else  if ($scope.TendorDetails.BidStartDate == null || $scope.TendorDetails.BidStartDate == undefined) {
            alert("First Select Bid Start Date");
            $scope.TendorDetails.FreezeDate = null;
            return false;
        }
        else if ($scope.TendorDetails.BidStartDate > $scope.TendorDetails.FreezeDate) {
            alert("Please select Freeze Date greater than Bid Start Date");
            $scope.TendorDetails.FreezeDate = null;
            return false;
        }
        else  if ($scope.TendorDetails.FreezeDate == null || $scope.TendorDetails.FreezeDate == undefined) {
            alert("First Select Freeze Date");
            $scope.TendorDetails.TechBidOpenDate = null;
            return false;
        }
        else if ($scope.TendorDetails.FreezeDate > $scope.TendorDetails.TechBidOpenDate) {
            alert("Please select Technical Bid Open Date greater than Freeze Date");
            $scope.TendorDetails.TechBidOpenDate = null;
            return false;
        }
        else  if ($scope.TendorDetails.TechBidOpenDate == null || $scope.TendorDetails.TechBidOpenDate == undefined) {
            alert("First Select Technical Bid Open Date");
            $scope.TendorDetails.FinancialBidOpenDate = null;
            return false;
           
        }
        else if ($scope.TendorDetails.TechBidOpenDate > $scope.TendorDetails.FinancialBidOpenDate) {
            alert("Please select Financial Bid Open Date greater than Technical Bid Open Date");
            $scope.TendorDetails.FinancialBidOpenDate = null;
            return false;
            
           
        }
        //else  if ($scope.TendorDetails.ClarificationStartDate == null || $scope.TendorDetails.ClarificationStartDate == undefined) {
        //    alert("First Select Clarification Start Date");
        //    $scope.TendorDetails.ClarificationEndDate = null;
        //    return false;
        //}
        //else if ($scope.TendorDetails.ClarificationStartDate > $scope.TendorDetails.ClarificationEndDate) {
        //    alert("Please select Clarification End Date greater than Clarification Start Date");
        //    $scope.TendorDetails.ClarificationEndDate = null;
        //    return false;
        //}
        else {
            return true;
        }
    };
    

    $scope.getTheFilesTendorNotice = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append("tendornotice", value);
        });
    };

    $scope.getTheFilesTendorDoc = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append("tendordoc", value);
        });
    };

    $scope.getTheFilesBOQFile = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append("boqfile", value);
        });
    };
    
    $scope.uploadtech = function (ID) {
        formdata.append("ID", ID);
        var request = {
            method: 'POST',
            url: '/Admin/CreateTendor/UploadFiles/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };
        $http(request)
                   .success(function (d) {
                       if (d.msg == "success") {
                           if ($scope.Action == "Create Tendor") {
                               alert("Tendor is successfully created ");
                           }
                           else if ($scope.Action == "Update")
                           {
                               alert("Tendor is successfully updated ");
                               $scope.Action = "Create Tendor";
                           }
                           $scope.GetAllTendor();
                           $scope.ViewList = true;
                           $scope.SubmitTendor = false;
                           $scope.CreateTendorForm = false;
                       }
                       else {
                           if ($scope.Action == "Create Tendor") {

                               alert(d.msg);
                               alert('File Upload Failed');
                           }
                           else if ($scope.Action == "Update")
                           {
                               alert("Tendor is successfully updated ");
                               $scope.Action = "Create Tendor";
                               $scope.GetAllTendor();
                               $scope.ViewList = true;
                               $scope.SubmitTendor = false;
                               $scope.CreateTendorForm = false;
                           }
                       }
                   })
                   .error(function () {
                   });
    }
   
    $scope.BackToList = function () {
        $scope.ViewList = true;
        $scope.SubmitTendor = false;
        $scope.CreateTendorForm = false;
    };


    $scope.MovetoCreate = function () {
        var file1 = getNameFromPath($("#tendornotice").val());
        var file2 = getNameFromPath($("#tendordoc").val());
        var file3 = getNameFromPath($("#boqfile").val());
        $scope.filename1 = file1;
        $scope.filename2 = file2;
        $scope.filename3 = file3;

        if ($scope.Action == "Create Tendor") {
            
            if (file1 == null || file2 == null || file3 == null)
            {
                alert("Please Select All Files to be Uploaded");
            }
            else{
                var extension1 = file1.substr((file1.lastIndexOf('.') + 1));
                var extension2 = file2.substr((file2.lastIndexOf('.') + 1));
                var extension3 = file3.substr((file3.lastIndexOf('.') + 1));

                if (extension1 != "pdf" )
                { 
                    alert("Only pdf files are allowed as Tender Notice ");
                    formdata.delete("tendornotice");
                }
                else if(extension2 != "pdf")
                {
                    alert("Only pdf files are allowed as Tender Document ");
                    formdata.delete("tendordoc");
                }
                else if (extension3 != "xls") {
                    alert("Only xls files are allow for BOQ file");
                    formdata.delete("boqfile");
                }
                else {
                    $scope.ViewList = false;
                    $scope.SubmitTendor = true;
                    $scope.CreateTendorForm = false;
            }

            
            }
        }
        else {
            
            if (file1 != null)
            {
                var extension1 = file1.substr((file1.lastIndexOf('.') + 1));
                if (extension1 != "pdf") {
                    alert("Only pdf files are allowed as Tender Notice ");
                    formdata.delete("tendornotice");
                    return;
                }
            }
            if (file2 != null)
            {
                var extension2 = file2.substr((file2.lastIndexOf('.') + 1));
                if (extension2 != "pdf") {
                    alert("Only pdf files are allowed as Tender Document ");
                    formdata.delete("tendordoc");
                    return;
                }
            }

            if (file3 != null) {
                var extension3 = file3.substr((file3.lastIndexOf('.') + 1));
                if (extension3 != "xls") {
                    alert("Only xls files are allow for BOQ file");
                    formdata.delete("boqfile");
                    return;
                }
            }
           
                $scope.ViewList = false;
                $scope.SubmitTendor = true;
                $scope.CreateTendorForm = false;
          
        }
    };


    $scope.Delete = function (TendorID) {
        var r = confirm("Are you sure, want to Delete Tendor");

        if (r == true) {


            TService.GetPermissionToUpdate(TendorID).then(function (d) {
                if (d.data.Status == 1)
                {
                    TService.DeleteTendor(TendorID).then(function (d2) {
                        if (d2.data.msg == "success") {
                            alert("Tendor is Deleted Successfully");
                            $scope.GetAllTendor();
                            $scope.SubmitTendor = false;
                            $scope.CreateTendorForm = false;
                            $scope.ViewList = true;
                        }
                        else {
                            alert("There is some problem in Deletion");
                        }
                    });
                }
                else {
                    alert("This Tendor is now Active. You does not have permission to Delete it.");
                }
            

            }, function () { });
        }
       
    };

});

Model.factory("TService", function ($http) {
    var fac = {};
    fac.GetDepartment = function () {
        return $http.get("/Admin/Department/GetAllDepartment");
    };


    fac.GetCategory = function () {
        return $http.get("/Admin/Category/GetAllCategory");
    };

    fac.GetAllTendor = function () {
        return $http.get("/Admin/CreateTendor/GetAllTendor");
    };

    fac.ADDCreateTendor = function (Tendor) {
        return $http.post("/Admin/CreateTendor/ADDTendor", Tendor);
    };

    fac.GetPermissionToUpdate = function (TendorID) {
        return $http.post("/Admin/CreateTendor/GetAccesstoUpdate?TendorID="+ TendorID);
    };

    fac.UpdateTendor = function (Tendor) {
        return $http.post("/Admin/CreateTendor/UpdateTendor", Tendor);
    };

    fac.DeleteTendor = function (TendorID) {
        return $http.post("/Admin/CreateTendor/DeleteTendor?TendorID="+ TendorID);
    };

    return fac;

});