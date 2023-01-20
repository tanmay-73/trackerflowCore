// <reference path="J:\CASEPOINT\Project\TrackerFlow\Web\TrackerFlow.UI\Views/Dash/DSetting.cshtml" />
// <reference path="J:\CASEPOINT\Project\TrackerFlow\Web\TrackerFlow.UI\Views/Dash/DSetting.cshtml" />
$(document).ready(function () {

    // ***************************************
    // Initially all validations are hidden
    // ***************************************        
    $('#addofficeCheck').hide();
    $('#addofficeCheck').hide();
    $('#addareaCheck').hide();
    $('#officeCheck').hide();
    $('#officeCheckTwo').hide();
    $('#floorCheck').hide();
    $('#emailCheck').hide();
    // ***************************************
    // On Change validations :
    // ***************************************        
    $('#add_Office').change(function () {
        validateAddOffice();
    });
    $('#add_Floor').change(function () {
        validateAddFloor();
    });
    $('#add_Area').change(function () {
        validateAddArea();
    });
    $('#add_Email').change(function () {
        validateAddEmail();
    });
    $('#select_OfficeOne').change(function () {
        validateSelectOffice();
    });
    $('#select_Office').change(function () {
        validateSelectOfficeTwo();
    });
    $('#select_Floor').change(function () {
        validateSelectFloor();
    });
});


// ***************************************
// Add Office Validations
// ***************************************
function validateAddOffice() {
    var officeValue = $('#add_Office').val();
    var office_format = /^[a-zA-Z]*$/
    if (officeValue.length > 50) {
        $('#addofficeCheck').show();
        $('#addofficeCheck').html("Please enter no more than 50 characters");
        return false;
    }
    else if (officeValue != '' && office_format.test(officeValue)) {
        $('#addofficeCheck').hide();
        return true;
    }
    else {
        $('#addofficeCheck').show();
        $('#addofficeCheck').html("Please enter valid Office Name (Only letters are allowed)");
        return false;
    }
}
// ***************************************
// Add Floor Validations
// ***************************************
function validateAddFloor() {
    var floorValue = $('#add_Floor').val();
    if (floorValue != '') {
        $('#addfloorCheck').hide();
        return true;
    }
    else {
        $('#addfloorCheck').show();
        $('#addfloorCheck').html("Please enter valid Floor Number");
        return false;
    }
}
// ***************************************
// Add Area Validations
// ***************************************
function validateAddArea() {
    var areaValue = $('#add_Area').val();
    if (areaValue.length > 50) {
        $('#addareaCheck').show();
        $('#addareaCheck').html("Please enter no more than 50 characters");
        return false;
    }
    else if (areaValue != '') {
        $('#addareaCheck').hide();
        return true;
    }
    else {
        $('#addareaCheck').show();
        $('#addareaCheck').html("Please enter valid Area Name");
        return false;
    }
}
// ***************************************
// Add Email Validations
// ***************************************
function validateAddEmail() {
    var emailValue = $('#add_Email').val();
    var email_format = /\S+@\S+\.\S+/;
    if (emailValue != '' && email_format.test(emailValue)) {
        $('#emailCheck').hide();
        return true;
    }
    else {
        $('#emailCheck').show();
        $('#emailCheck').html("Please enter valid Email Address (Format - username@gmail.com)");
        return false;
    }
}
// ***************************************
// Select Office Validations
// ***************************************
function validateSelectOffice() {
    var officeValue = $('#select_OfficeOne').val();
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
// Select Office Two Validations
// ***************************************
function validateSelectOfficeTwo() {
    var officeValue = $('#select_Office').val();
    if (officeValue != '') {
        $('#officeCheckTwo').hide();
        return true;
    }
    else {
        $('#officeCheckTwo').show();
        $('#officeCheckTwo').html("Please select Office");
        return false;
    }
}
// ***************************************
// Select Floor Validations
// ***************************************
function validateSelectFloor() {
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
// ****************************
// If all validations are true 
// ****************************
function validateOffice() {
    if (validateAddOffice()) {
        //alert("Office Added Successfully");
        return AddOffice();
    }
}

function validateFloor() {
    if (validateSelectOffice() && validateAddFloor()) {
        //alert("Floor Added Successfully");
        return AddFloor();
    }
}

function validateArea() {
    if (validateSelectOfficeTwo() && validateSelectFloor() && validateAddArea()) {
        //alert("Area Added Successfully");
        return AddArea();
    }
}

function validateEmail() {
    if (validateAddEmail()) {
        //alert("Email Added Successfully");
        return AddEmailID();
    }
}

// ****************************
// Add Office 
// ****************************
function AddOffice() {
    var Addofc = {

        c_ofcname: $('#add_Office').val()
    }
    console.log(Addofc);

    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + '/api/Settings/InsOfc',
        data: JSON.stringify(Addofc),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != true) {
                document.getElementById("warningMessage").innerHTML = "Office Already Exist";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
                return;
            }
            document.getElementById("successMessage").innerHTML = "Inserted Successfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            window.location.reload();
        }
    });
}

// ****************************
// Add Floor 
// ****************************
function AddFloor() {
    var Addflr = {
        c_ofcid: $('#select_OfficeOne').val(),
        c_floornum: $('#add_Floor').val()
    }
    console.log(Addflr);

    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + '/api/Settings/InsFlr/' + $('#select_OfficeOne').val(),
        data: JSON.stringify(Addflr),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != true) {
                document.getElementById("warningMessage").innerHTML = "Floor Already Exist";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
                return;
            }
            document.getElementById("successMessage").innerHTML = "Inserted Successfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            window.location.reload();
        }
    });
}

// ****************************
// Add Area 
// ****************************
function AddArea() {
    var Addare = {
        c_floorid: $('#select_Floor').val(),
        c_areaname: $('#add_Area').val()
    }
    debugger;
    console.log(Addare);

    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + '/api/Settings/InsAre/' + $('#select_Floor').val(),
        data: JSON.stringify(Addare),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != true) {
                document.getElementById("warningMessage").innerHTML = "Area Already Exist";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
                return;
            }
            document.getElementById("successMessage").innerHTML = "Inserted Successfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            window.location.reload();
        }
    });
}

// ****************************
// Add EmailID 
// ****************************
function AddEmailID() {
    var Addeid = {

        c_email: $('#add_Email').val()
    }
    console.log(Addeid);
    debugger;
    alert("aa");
    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + '/api/Settings/InsEmailId',
        data: JSON.stringify(Addeid),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != true) {
                document.getElementById("warningMessage").innerHTML = "EmailID Already Exist";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
                return;
            }
            document.getElementById("successMessage").innerHTML = "Inserted Successfully";
            document.getElementById("msgSuccess").style.display = "block";
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
            }, 3000);
            window.location.reload();
        }
    });
}