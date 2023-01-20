function myFunction() {
    $("#upload:hidden").trigger('click');
}

//function addPath() {
//    document.getElementById('Filepath').value = this.value;
//}

function myFunction2() {
    $("#submit:hidden").trigger('click');
    ExportToTable();
}
$(document).ready(function () {
    /*$('.grid-wrapper').hide();*/
    // ***************************************
    // Date Picker JS
    // ***************************************
    var presentDate = new Date();
    $("#datepicker").kendoDatePicker({
        value: presentDate,
        disableDates: function (date) {
            if (date < presentDate) {
                return true;
            } else {
                return false;
            }
        }
    });
    // ***************************************
    // Buttons Active/Inactive JS
    // ***************************************
    $('#submitAudit').prop('disabled', true);

    // ***************************************
    // Search functionality JS
    // ***************************************
    $('#txtSearchString').on('input', function () {
        var q = $("#txtSearchString").val();
        var grid = $("#categoryGrid").data("kendoGrid");
        grid.dataSource.query({
            page: 1,
            pageSize: 8,
            filter: {
                logic: "or",
                filters: [
                    { field: "c_assetname", operator: "contains", value: q },
                    { field: "c_uniqueid", operator: "contains", value: q },
                    { field: "c_areaname", operator: "contains", value: q },
                    { field: "c_officename", operator: "contains", value: q },
                    { field: "c_floornum", operator: "contains", value: q },
                    { field: "c_date", operator: "contains", value: q }
                ]
            }
        });
    });

    // ***************************************
    // Initially all validations are hidden
    // ***************************************
    $('#assetnameCheck').hide();
    $('#uniqueidCheck').hide();
    $('#datepickerCheck').hide();
    $('#officeCheck').hide();
    $('#floorCheck').hide();
    $('#areaCheck').hide();
    // ***************************************
    // On Change validations :
    // ***************************************
    $('#assetname').change(function () {
        validateAssetName();
    });
    $('#uniqueid').change(function () {
        validateUniqueId();
    });
    $('#datepicker').change(function () {
        validateDate();
    });
    $('#select_Office').change(function () {
        validateOffice();
    });
    $('#select_Floor').change(function () {
        validateFloor();
    });
    $('#select_Area').change(function () {
        validateArea();
    });
    $('#editassetname').change(function () {
        validateeAssetName();
    });
    $('#edituniqueid').change(function () {
        validateeUniqueId();
    });
    $('#editdatepicker').change(function () {
        validateeDate();
    });
    $('#editselect_Office').change(function () {
        validateeOffice();
    });
    $('#editselect_Floor').change(function () {
        validateeFloor();
    });
    $('#editselect_Area').change(function () {
        validateeArea();
    });
});


// ***************************************
// Asset Name Validations
// ***************************************
function validateAssetName() {
    var avalue = $('#assetname').val();
    if (avalue != '') {
        $('#assetnameCheck').hide();
        return true;
    }
    else {
        $('#assetnameCheck').show();
        $('#assetnameCheck').html("Please enter asset name");
        return false;
    }
}

// ***************************************
// Unique Id Validations
// ***************************************
function validateUniqueId() {
    var uidvalue = $('#uniqueid').val();
    if (uidvalue != '') {
        $('#uniqueidCheck').hide();
        return true;
    }
    else {
        $('#uniqueidCheck').show();
        $('#uniqueidCheck').html("Please enter Unique ID");
        return false;
    }
}

// ***************************************
// Date Validations
// ***************************************
function validateDate() {
    var dateValue = $('#datepicker').val();
    if (dateValue != '') {
        $('#datepickerCheck').hide();
        return true;
    }
    else {
        $('#datepickerCheck').show();
        $('#datepickerCheck').html("Please select Purchase Date Of Asset");
        return false;
    }
}
// ***************************************
// Office Validations
// ***************************************
function validateOffice() {
    var officeValue = $('#select_Office').val();
    if (officeValue != '') {
        $('#officeCheck').hide();
        return true;
    }
    else {
        $('#officeCheck').show();
        $('#officeCheck').html("Please select Office");
        return false;
    }
}
// ***************************************
// Floor Validations
// ***************************************
function validateFloor() {
    var floorValue = $('#select_Floor').val();
    if (floorValue != '') {
        $('#floorCheck').hide();
        return true;
    }
    else {
        $('#floorCheck').show();
        $('#floorCheck').html("Please select Floor");
        return false;
    }
}
// ***************************************
// Area Validations
// ***************************************
function validateArea() {
    var areaValue = $('#select_Area').val();
    if (areaValue != '') {
        $('#areaCheck').hide();
        return true;
    }
    else {
        $('#areaCheck').show();
        $('#areaCheck').html("Please select Area");
        return false;
    }
}

//Validations for Edit Popup

// ***************************************
// Asset Name Validations
// ***************************************
function validateeAssetName() {
    var avalue = $('#editassetname').val();
    if (avalue != '') {
        $('#editassetnameCheck').hide();
        return true;
    }
    else {
        $('#editassetnameCheck').show();
        $('#editassetnameCheck').html("Please enter asset name");
        return false;
    }
}

// ***************************************
// Unique Id Validations
// ***************************************
function validateeUniqueId() {
    var uidvalue = $('#edituniqueid').val();
    if (uidvalue != '') {
        $('#edituniqueidCheck').hide();
        return true;
    }
    else {
        $('#edituniqueidCheck').show();
        $('#edituniqueidCheck').html("Please enter Unique ID");
        return false;
    }
}

// ***************************************
// Date Validations
// ***************************************
function validateeDate() {
    var dateValue = $('#editdatepicker').val();
    if (dateValue != '') {
        $('#editdatepickerCheck').hide();
        return true;
    }
    else {
        $('#editdatepickerCheck').show();
        $('#editdatepickerCheck').html("Please select Purchase Date Of Asset");
        return false;
    }
}
// ***************************************
// Office Validations
// ***************************************
function validateeOffice() {
    var officeValue = $('#editselect_Office').val();
    if (officeValue != '') {
        $('#editofficeCheck').hide();
        return true;
    }
    else {
        $('#editofficeCheck').show();
        $('#editofficeCheck').html("Please select Office");
        return false;
    }
}
// ***************************************
// Floor Validations
// ***************************************
function validateeFloor() {
    var floorValue = $('#editselect_Floor').val();
    if (floorValue != '') {
        $('#editfloorCheck').hide();
        return true;
    }
    else {
        $('#editfloorCheck').show();
        $('#editfloorCheck').html("Please select Floor");
        return false;
    }
}
// ***************************************
// Area Validations
// ***************************************
function validateeArea() {
    var areaValue = $('#editselect_Area').val();
    if (areaValue != '') {
        $('#editareaCheck').hide();
        return true;
    }
    else {
        $('#editareaCheck').show();
        $('#editareaCheck').html("Please select Area");
        return false;
    }
}



// ****************************
// If all validations are true 
// ****************************
function validateData() {
    var valDate = validateDate();
    var valOffice = validateOffice();
    var valFloor = validateFloor();
    var valArea = validateArea();
    var valaname = validateAssetName();
    var valuid = validateUniqueId();
    if (valDate && valOffice && valFloor && valArea && valaname && valuid) {
        /*$('#submitAudit').prop('disabled', false);*/
        /*$('.empty-record, .grid-wrapper').toggle();*/
        addAsset();
        //$("#savebtn").click(function () {            
        //});
        //Grid
    }
}
function validateEdit() {
    var valDate = validateeDate();
    var valOffice = validateeOffice();
    var valFloor = validateeFloor();
    var valArea = validateeArea();
    var valaname = validateeAssetName();
    var valuid = validateeUniqueId();
    if (valDate && valOffice && valFloor && valArea && valaname && valuid) {
        /*$('#submitAudit').prop('disabled', false);*/
        /*$('.empty-record, .grid-wrapper').toggle();*/
        updateAsset();
        //$("#savebtn").click(function () {            
        //});
        //Grid
    }
}
//$('#addExcel').change(function () {
//    $('#Filepath').val(this.value);
//    /*document.getElementById('Filepath').value = this.value;*/
//});

//// Open file browse dialog box                       
//$("#addExcel").click(function (e) {
//    e.preventDefault();
//    $("#upload:hidden").trigger('click');
//});

////After selecting a file from dialog box
//$("#importExcel").click(function () {
//    $("#submit:hidden").trigger('click');
//    ExportToTable();
//});

//Search functionality js
$('#txtSearchString').on('input', function () {
    var q = $("#txtSearchString").val();
    var grid = $("#categoryGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 20,
        filter: {
            logic: "or",
            filters: [
                { field: "c_assetid", operator: "contains", value: q },
                { field: "c_assetname", operator: "contains", value: q },
                { field: "c_uniqueid", operator: "contains", value: q },
                { field: "c_oid", operator: "contains", value: q },
                { field: "c_fid", operator: "contains", value: q },
                { field: "c_aid", operator: "contains", value: q }
            ]
        }
    });
});

// Dropdown js
//var office = $("#select_Office").kendoDropDownList({
//    optionLabel: "Select Office...",
//    dataTextField: "c_ofcname",
//    dataValueField: "c_ofcid",
//    dataSource: {
//        type: "odata",
//        serverFiltering: true,
//        transport: {
//            read: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Categories"
//        }
//    }
//}).data("kendoDropDownList");

//var floor = $("#select_Floor").kendoDropDownList({
//    autoBind: false,
//    cascadeFrom: "select_Office",
//    optionLabel: "Select Floor...",
//    dataTextField: "c_floornum",
//    dataValueField: "c_floorid",
//    dataSource: {
//        type: "odata",
//        serverFiltering: true,
//        transport: {
//            read: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Products"
//        }
//    }
//}).data("kendoDropDownList");

//var area = $("#select_Area").kendoDropDownList({
//    autoBind: false,
//    cascadeFrom: "select_Floor",
//    optionLabel: "Select Area...",
//    dataTextField: "c_areaname",
//    dataValueField: "c_aid",
//    dataSource: {
//        type: "odata",
//        serverFiltering: true,
//        transport: {
//            read: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Order_Details?$expand=Order"
//        }
//    }
//}).data("kendoDropDownList");

//$("#get").click(function() {
//    var categoryInfo = "\nCategory: { id: " + categories.value() + ", name: " + categories.text() + " }",
//        productInfo = "\nProduct: { id: " + products.value() + ", name: " + products.text() + " }",
//        orderInfo = "\nOrder: { id: " + orders.value() + ", name: " + orders.text() + " }";

//    alert("Order details:\n" + categoryInfo + productInfo + orderInfo);
//});


function ExportToTable() {
    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
    //var filename1 = Path.GetFileName($("#upload").val());
    //alert(Path.GetFileName($("#upload").val()));
    /*Checks whether the file is a valid excel file*/
    if (regex.test($("#upload").val().toLowerCase())) {
        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
        $("#Filepath").val($("#upload").val());
        if ($("#upload").val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;
        }
        /*Checks whether the browser supports HTML5*/
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();
            reader.onload = function (e) {
                console.log("if");
                var data = e.target.result;
                console.log("data");
                console.log(data);


                /*Converts the excel data in to object*/
                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                    console.log("workbook");
                    console.log(workbook);
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }
                /*Gets all the sheetnames of excel in to a variable*/
                var sheet_name_list = workbook.SheetNames;
                console.log(sheet_name_list);
                var cnt = 0; /*This is used for restricting the script to consider only first sheet of excel*/
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                    /*Convert the cell value to Json*/
                    if (xlsxflag) {
                        var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        console.log("exceljson");
                        console.log("exceljson");
                        console.log(exceljson);
                        // Send json array
                        $.ajax({
                            type: "POST",
                            url: API_URL + "/api/ApiAudit/SendExcel",
                            data: JSON.stringify(exceljson),
                            contentType: "application/json",
                            success: function (result) {
                                document.getElementById("successMessage").innerHTML = "Data Imported Successfully";
                                document.getElementById("msgSuccess").style.display = "block";
                                setTimeout(function () {
                                    $("#msgSuccess").fadeOut(1000)
                                    window.location.reload()
                                }, 3000);
                            },
                            error: function (result, status) {
                                console.log(result);
                            }
                        });
                        console.log(exceljson);
                    }
                    else {
                        var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }
                    if (exceljson.length > 0 && cnt == 0) {
                        //BindTable(exceljson, '#exceltable');
                        cnt++;
                    }
                });
                $('#exceltable').show();
            }
            if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/
                reader.readAsArrayBuffer($("#upload")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#upload")[0].files[0]);
            }
        }
        else {
            alert("Sorry! Your browser does not support HTML5!");
        }
    }
    else {
        document.getElementById("warningMessage").innerHTML = "Please upload a valid Excel file!";
        document.getElementById("msgWarning").style.display = "block";
        setTimeout(function () {
            $("#msgWarning").fadeOut(1000)
        }, 3000);
        console.log(response);
        //alert("Please upload a valid Excel file!");
    }
}

//For adding asset through Pop-up
function addAsset() {
    var assetDetails = {
        c_assetname: $('#assetname').val(),
        c_uniqueid: $('#uniqueid').val(),
        c_date: $('#datepicker').val(),
        c_areaid: $('#select_Area').val(),
    }
    var settings = {
        "url": API_URL + "/api/ApiAudit/AddAsset",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(assetDetails),
    };
    console.log(JSON.stringify(assetDetails));
    $.ajax(settings).done(function (response) {
        if (response == true) {
            //alert("Successfully Update");
            //clearAllPopUp();        
            document.getElementById("successMessage").innerHTML = "Data Added Succesfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            $("#addcategory").data("kendoWindow").close();
            $("#categoryGrid").data("kendoGrid").dataSource.read();
            //window.location.reload();
            //maingrid();
        }
        else {
            document.getElementById("warningMessage").innerHTML = "There is some error";
            document.getElementById("msgWarning").style.display = "block";
            setTimeout(function () {
                $("#msgWarning").fadeOut(1000)
            }, 3000);
        }
    });
}


//Updating Asset Through Popup
function updateAsset() {
    var assetDetails = {
        c_assetname: $('#editassetname').val(),
        c_uniqueid: $('#edituniqueid').val(),
        c_date: $('#editdatepicker').val(),
        c_areaid: $('#editselect_Area').val(),
    }
    var settings = {
        "url": API_URL + "/api/ApiAudit/updateAsset",
        "method": "PUT",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(assetDetails),
    };
    console.log(JSON.stringify(assetDetails));
    $.ajax(settings).done(function (response) {
        if (response == true) {
            //alert("Successfully Update");
            //clearAllPopUp();        
            document.getElementById("successMessage").innerHTML = "Data Updated Succesfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            $("#editcategory").data("kendoWindow").close();
            $("#categoryGrid").data("kendoGrid").dataSource.read();
        }
        else {
            document.getElementById("warningMessage").innerHTML = "There is some error";
            document.getElementById("msgWarning").style.display = "block";
            setTimeout(function () {
                $("#msgWarning").fadeOut(1000)
            }, 3000);
        }
    });
}


////Multiple Delete
//function DeleteMultipleRow() {
//    var grid = $('#categoryGrid').data('kendoGrid');
//    var rows = grid.select();
//    var ids = [];
//    var i = 0;
//    rows.each(function (e) {
//        ids[i] = grid.dataItem(this).c_uniqueid;        
//        i++;
//        //SelectRows.push(id);
//    });    
//    if (i == 0) {
//        document.getElementById("warningMessage").innerHTML = "Please Select Atleast One";
//        document.getElementById("msgWarning").style.display = "block";
//        setTimeout(function () {
//            $("#msgSuccess").fadeOut(400)
//        }, 700);
//    }
//    else {
//        //$("#deletePopup").data("kendoWindow").center().open();
//        //$("#btnYesResponse").click(function () {            
//        //    deletecourse(ids);
//        //    alert('Deleted Successfully');
//        //    window.location.reload();
//        //    // toolbar: [{ text: "Refresh", className: "btn-refresh" }]
//        $("#deletePopup").data("kendoWindow").center().open();
//        $("#yusbtn").click(function () {
//            deletecourse(ids);
//        });
//            $("#nobtn").click(function () {
//                $("#deletePopup").data("kendoWindow").close();
//            });
//        //});
//        //$("#btnNoResponse").click(function () {

//        //    $("#deletePopup").data("kendoWindow").close();
//        //    window.location.reload();

//        //});
//    }        
//}
//function deletecourse(e) {
//    //for multiple delete              
//    if (Array.isArray(e)) {
//        for (var i = 0; i < e.length; i++) {
//            var settings = {
//                "url": API_URL + "/api/ApiAudit/delAsset",
//                "method": "PUT",
//                "timeout": 0,
//                "headers": {
//                    "Content-Type": "application/json"
//                },
//                "data": JSON.stringify(e[i]),
//                //"data": JSON.stringify(dataitem.id),
//            };
//            $.ajax(settings).done(function (response) {
//                if (response == true) {
//                    if (i == e.length) {
//                        $("#deletePopup").data("kendoWindow").close();
//                        document.getElementById("successMessage").innerHTML = "Assets Deleted Succesfully";
//                        document.getElementById("msgSuccess").style.display = "block";
//                        setTimeout(function () {
//                            $("#msgSuccess").fadeOut(5000)
//                        }, 5000);
//                        //window.location.reload();
//                    }                    
//                    setTimeout(function () {
//                        window.location.reload();
//                    }, 5000);
//                }
//                else {
//                    $("#deletePopup").data("kendoWindow").close();
//                    document.getElementById("warningMessage").innerHTML = "There is some error";
//                    document.getElementById("msgWarning").style.display = "block";
//                    setTimeout(function () {
//                        $("#msgWarning").fadeOut(1000)
//                    }, 3000);
//                }
//            });
//        }
//        // console.log(settings);
//    }
//    //for single delete
//    else {
//        var grid = $("#categoryGrid").data("kendoGrid");
//        var row = e.select().closest("tr");
//        var dataitem = grid.dataItem(row);
//        $("#deletePopup").data("kendoWindow").center().open();
//        $("#yusbtn").click(function () {
//            console.log(dataitem.id)
//            var settings = {
//                "url": API_URL + "/api/ApiAudit/delAsset",
//                "method": "PUT",
//                "timeout": 0,
//                "headers": {
//                    "Content-Type": "application/json"
//                },
//                "data": JSON.stringify(dataitem.id)
//                //"data": JSON.stringify(dataitem.id),
//            };
//            $.ajax(settings).done(function (response) {
//                if (response == true) {
//                    $("#deletePopup").data("kendoWindow").close();
//                    document.getElementById("successMessage").innerHTML = "Asset Deleted Succesfully";
//                    document.getElementById("msgSuccess").style.display = "block";
//                    setTimeout(function () {
//                        $("#msgSuccess").fadeOut(1000)
//                    }, 3000);                    
//                    //var grid = $('#categoryGrid').data('kendoGrid');
//                    //grid.dataSource.read();
//                    $("#deletePopup").data("kendoWindow").close();                    
//                    window.location.reload();
//                }
//                else {
//                    $("#deletePopup").data("kendoWindow").close();
//                    document.getElementById("warningMessage").innerHTML = "There is some error";
//                    document.getElementById("msgWarning").style.display = "block";
//                    setTimeout(function () {
//                        $("#msgWarning").fadeOut(1000)
//                    }, 3000);
//                }
//            });
//        });
//        $("#nobtn").click(function () {
//            $("#deletePopup").data("kendoWindow").close();
//            window.location.reload();

//        });
//    }
//}

