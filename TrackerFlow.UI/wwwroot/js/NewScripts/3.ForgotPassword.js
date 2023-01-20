$(document).ready(function () {

    // ***************************************
    // Initially all validations are hidden
    // ***************************************    
    $('#newpwdCheck').hide();
    $('#cnewpwdCheck').hide();

    // ***************************************
    // On Tab validations  :    
    // ***************************************    
    $('#loginPassword').change(function () {
        validatePwd();
    });
    $('#confirmLoginPassword').change(function () {
        validateConfirmPwd();
    });

});

// ***************************************
// Password Validations
// ***************************************
function validatePwd() {
    var pwdValue = $('#loginPassword').val();
    var pass_format = /^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
    if (pass_format.test(pwdValue) && pwdValue != '') {
        $('#newpwdCheck').hide();
        return true;
    }
    else {
        $('#newpwdCheck').show();
        $('#newpwdCheck').html("Please enter valid Password (Use 8 or more characters with a mix of letters, numbers & symbols)");
        return false;
    }
}

// ***************************************
// Confirm Password Validations
// ***************************************
function validateConfirmPwd() {
    var pwdValue = $('#loginPassword').val();
    var cpwdValue = $('#confirmLoginPassword').val();
    if (cpwdValue == '') {
        $('#cnewpwdCheck').show();
        $('#cnewpwdCheck').html("Please enter valid Confirm Password");
        return false;
    }
    else if (cpwdValue != pwdValue) {
        $('#cnewpwdCheck').show();
        $('#cnewpwdCheck').html("Password and Confirm password does not match");
        return false;
    }
    else {
        $('#cnewpwdCheck').hide();
        return true;
    }
}

// ******************************************************************************
// If all validations are true than Password Reset is successfull
// ******************************************************************************
function validateData() {

    var valPwd = validatePwd();
    var valCpwd = validateConfirmPwd();

    if (valPwd && valCpwd) {
        //alert("Password Reset Successful Successful");
        var url = window.location.href;
        UpdateForgotPassword(url.toString().replace(UI_URL + "/Sign/Reset?", "")); //Token is sent eg.ABCDEF
    }
}

// ***************************************
// AJAX 
// ***************************************
function UpdateForgotPassword(tokenValue) {
    var UpdateForgotPassword = { UpdateForgotPassword: $('#loginPassword').val(), Token: tokenValue } // password and token are sent

    console.log(UpdateForgotPassword);
    //var mess = $("#username");

    var settings =
    {
        "url": API_URL + "/api/ForgotPasswordAPI/UpdatePassword",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(UpdateForgotPassword),
    };
    console.log(API_URL);
    $.ajax(settings).done(function (response) {
        if (response == 1) { // success
            document.getElementById("successMessage").innerHTML = "Password Update Successfully";
            document.getElementById("msgSuccess").style.display = "block";
            $("#changepasswordid").html("Plese Wait");
            $("#changepasswordid").prop("disabled", true);
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
                window.location = UI_URL + "/Sign/login";
            }, 3000);
        }
        else if (response == -1) // changed token
        {
            document.getElementById("successMessage").innerHTML = "Please do not change token in URL.";
            document.getElementById("msgSuccess").style.display = "block";
            $("#changepasswordid").html("Plese Wait");
            $("#changepasswordid").prop("disabled", true);
            setTimeout(function () {
                $("#msgSuccess").fadeOut(1000)
                window.location = UI_URL + "/Sign/login";
            }, 3000);
        }
        else if (response == -2) // entered forgot password and database password are same
        {
            document.getElementById("warningMessage").innerHTML = "Please enter new password which should be different from earlier password";
            document.getElementById("msgWarning").style.display = "block";
            $("#changepasswordid").html("Plese Wait");
            $("#changepasswordid").prop("disabled", true);
            setTimeout(function () {
                $("#msgWarning").fadeOut(1000)
                window.location = UI_URL + "/Sign/login";
            }, 3000);
        }
        else { // token expired through timestamp
            //alert('Token expired or changed');
            document.getElementById("warningMessage").innerHTML = "Token expired";
            document.getElementById("msgWarning").style.display = "block";
            setTimeout(function () {
                $("#msgWarning").fadeOut(1000)
                window.location = UI_URL + "/Sign/login";
            }, 3000);
        }
    });

}