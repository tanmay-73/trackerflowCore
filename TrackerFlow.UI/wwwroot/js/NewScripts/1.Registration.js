$(document).ready(function () {

    // ***************************************
    // Initially all validations are hidden
    // ***************************************
    $('#fnameCheck').hide();
    $('#lnameCheck').hide();
    $('#emailCheck').hide();
    $('#pwdCheck').hide();
    $('#cpwdCheck').hide();
    $('#chkCheck').hide();

    // ***************************************
    // On Tab validations :
    // ***************************************
    $('#registerFname').change(function () {
        validateFirstName();
    });
    $('#registerLname').change(function () {
        validateLastName();
    });
    $('#registerEmail').change(function () {
        validateEmail();
    });
    $('#registerpwd').change(function () {
        validatePwd();
    });
    $('#registerConfirmpwd').change(function () {
        validateConfirmPwd();
    });
});

// ***************************************
// First Name Validations
// ***************************************
function validateFirstName() {
    var fnameValue = $('#registerFname').val();
    var fname_format = /^[a-zA-Z]*$/
    if (fnameValue != '' && fname_format.test(fnameValue)) {
        $('#fnameCheck').hide();
        return true;
    }
    else {
        $('#fnameCheck').show();
        $('#fnameCheck').html("Please enter valid First Name (Only letters are allowed)");
        return false;
    }
}

// ***************************************
// Last Name Validations
// ***************************************
function validateLastName() {
    var lnameValue = $('#registerLname').val();
    var lname_format = /^[a-zA-Z]*$/
    if (lnameValue != '' && lname_format.test(lnameValue)) {
        $('#lnameCheck').hide();
        return true;
    }
    else {
        $('#lnameCheck').show();
        $('#lnameCheck').html("Please enter valid Last Name (Only letters are allowed)");
        return false;
    }
}

// ***************************************
// Email Validations
// ***************************************
function validateEmail() {
    var emailValue = $('#registerEmail').val();
    var mail_format = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
    if (mail_format.test(emailValue) && emailValue != '') {
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
// Password Validations
// ***************************************
function validatePwd() {
    var pwdValue = $('#registerpwd').val();
    var pass_format = /^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
    if (pass_format.test(pwdValue) && pwdValue != '') {
        $('#pwdCheck').hide();
        return true;
    }
    else {
        $('#pwdCheck').show();
        $('#pwdCheck').html("Please enter valid Password (Use 8 or more characters with a mix of letters, numbers & symbols)");
        return false;
    }
}

// ***************************************
// Confirm Password Validations :
// ***************************************
function validateConfirmPwd() {
    var pwdValue = $('#registerpwd').val();
    var cpwdValue = $('#registerConfirmpwd').val();
    if (cpwdValue == '') {
        $('#cpwdCheck').show();
        $('#cpwdCheck').html("Please enter valid Confirm Password");
        return false;
    }
    else if (cpwdValue != pwdValue) {
        $('#cpwdCheck').show();
        $('#cpwdCheck').html("Password and Confirm password does not match");
        return false;
    }
    else {
        $('#cpwdCheck').hide();
        return true;
    }
}

// ***************************************
// License Validations (Check Box)
// ***************************************
function validateCheckBox() {
    if ($('input[id=acceptTs]:checked').length == 0) {
        $('#chkCheck').show();
        $('#chkCheck').html("**Please accept the Terms & Policies");
        return false;
    }
    else {
        $('#chkCheck').hide();
        return true;
    }
}

// ****************************
// If all validations are true 
// ****************************
function validateData() {

    var valFname = validateFirstName();
    var valLname = validateLastName();
    var valEmail = validateEmail();
    var valPwd = validatePwd();
    var valCpwd = validateConfirmPwd();
    var valCheckbox = validateCheckBox();


    if (valFname && valLname && valEmail && valPwd && valCpwd && valCheckbox) {
        //alert("Registered Successfully");
        return Register();
    }
}
// ***************************************
// AJAX 
// ***************************************
function Register() {
    var UserClass = {
        c_id: $('#txtid').val(),
        c_fname: $('#registerFname').val(),
        c_lname: $('#registerLname').val(),
        c_email: $('#registerEmail').val(),
        c_password: $('#registerpwd').val(),
        c_cpassword: $('#registerConfirmpwd').val()
    }
    console.log(UserClass);

    $.ajax({
        async: true,
        type: 'POST',
        url: API_URL + '/api/register/reg',
        data: JSON.stringify(UserClass),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response == true) {
                //alert("Registration Successful");
                // Successmsg();
                //console.log(response.data);
                document.getElementById("successMessage").innerHTML = "Registered Successfully";
                document.getElementById("msgSuccess").style.display = "block";
                setTimeout(function () {
                    $("#msgSuccess").fadeOut(1000)
                    window.location = UI_URL + "/sign/login";
                }, 3000);
                $("#suc").html("Please Wait");
                $("#suc").prop("disabled", true);

            }
            else {
                //alert("Email Id already registered");
                //console.log(response);
                document.getElementById("warningMessage").innerHTML = "Email ID Already Exist";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
            }
        },
        error: function () {
            document.getElementById("errorMessage").innerHTML = "There is some error";
            document.getElementById("msgErrorr").style.display = "block";
            setTimeout(function () {
                $("#msgErrorr").fadeOut(1000)
                window.location = UI_URL + "/sign/index";
            }, 3000);
        }
    });
}