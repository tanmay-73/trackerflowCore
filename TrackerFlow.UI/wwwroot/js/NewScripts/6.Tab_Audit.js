$(document).ready(function () {

    $('.grid-wrapper').hide();
    // ***************************************
    // Date Picker JS
    // ***************************************
    //var presentDate = new Date();
    //$("#datepicker").kendoDatePicker({
    //    value: presentDate,
    //    disableDates: function (date) {
    //        if (date > presentDate) {
    //            return true;
    //        } else {
    //            return false;
    //        }
    //    }
    //});
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
            pageSize: 10,
            filter: {
                logic: "or",
                filters: [
                    { field: "c_assetname", operator: "contains", value: q }, //Model Name
                    { field: "c_uniqueid", operator: "contains", value: q }
                ]
            }
        });
    });
    // ***************************************
    // Initially all validations are hidden
    // ***************************************
    $('#datepickerCheck').hide();
    $('#officeCheck').hide();
    $('#floorCheck').hide();
    $('#areaCheck').hide();
    // ***************************************
    // On Change validations :
    // ***************************************
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
});
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
        $('#datepickerCheck').html("Please select Date Of Audit");
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
// ****************************
// If all validations are true 
// ****************************
function validateData() {
    var valDate = validateDate();
    var valOffice = validateOffice();
    var valFloor = validateFloor();
    var valArea = validateArea();

    if (valDate && valOffice && valFloor && valArea) {
        $('#submitAudit').prop('disabled', false);
        $('.empty-record, .grid-wrapper').toggle();
        $("#startAudit").click(function () {
            $(".empty-record").hide();
            $(".grid-wrapper").show();
        });
        //Grid
    }
}
