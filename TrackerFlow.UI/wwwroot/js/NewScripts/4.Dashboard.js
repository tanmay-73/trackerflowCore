$(document).ready(function () {

    // ***************************************
    // Initially all validations are hidden
    // ***************************************
    $('#fnameCheck').hide();
    $('#lnameCheck').hide();
    $('#mnumberCheck').hide();
    $('#oldpwdCheck').hide();
    $('#newpwdCheck').hide();
    $('#cnewpwdCheck').hide();

    // ***************************************
    // On Tab validations  :
    // ***************************************    
    $('#fname').change(function () {
        validateFirstName();
    });
    $('#lname').change(function () {
        validateLastName();
    });
    $('#mnumber').change(function () {
        validatePhoneNumber();
    });
    $('#curpassword').change(function () {
        validateOldPwd();
    });
    $('#newpassword').change(function () {
        validateNewPwd;
    });
    $('#conpassword').change(function () {
        validateConfirmNewPwd();
    });
});

// ***************************************    
// First Name Validations
// ***************************************
function validateFirstName() {
    var fnameValue = $('#fname').val();
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
    var lnameValue = $('#lname').val();
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
// Phone Number Validations
// ***************************************
function validatePhoneNumber() {
    let mnumberValue = $('#mnumber').val();
    if (mnumberValue == '') {
        $('#mnumberCheck').hide();
        return true;
    }
    else if (mnumberValue.length != 10) {
        $('#mnumberCheck').show();
        $('#mnumberCheck').html("Please enter valid Phone Number (10 digits only)");
        return false;
    }
    else {
        $('#mnumberCheck').hide();
        return true;
    }
}

// ***************************************
// Old Password Validations
// ***************************************
function validateOldPwd() {
    if ($("#curpassword").val() == '') {
        $('#oldpwdCheck').show();
        $('#oldpwdCheck').html("Please enter valid Password");
        return false;
    }
    else {
        $('#oldpwdCheck').hide();
        return true;
    }
}
// ***************************************
// New Password Validations
// ***************************************
function validateNewPwd() {
    var newpwdValue = $('#newpassword').val();
    var pass_format = /^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
    if (pass_format.test(newpwdValue) && newpwdValue != '') {
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
// New Confirm Password Validations
// ***************************************
function validateConfirmNewPwd() {
    var pwdValue = $('#newpassword').val();
    var cpwdValue = $('#conpassword').val();
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
