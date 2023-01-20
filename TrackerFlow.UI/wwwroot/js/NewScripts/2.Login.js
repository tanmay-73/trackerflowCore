$(document).ready(function () {

    // ***************************************
    // Retrieving Cookie
    // ***************************************
    var emailcookie = getCookie('trackerflowemail');
    var passcookie = getCookie('trackerflowpassword');
    if (emailcookie && passcookie != null) {
        $('#rememeberme').prop('checked', true);
    }
    $('#loginEmail').val(emailcookie);
    $('#loginPassword').val(passcookie);
    var emailcookie = getCookie('trackerflowemail');
    var passcookie = getCookie('trackerflowpassword');
    console.log(emailcookie);
    console.log(passcookie);
    if (emailcookie && passcookie != null) {
        $('#rememeberme').prop('checked', true);
    }
    $('#loginEmail').val(emailcookie);
    $('#loginPassword').val(passcookie);
    // ***************************************
    // Initially all validations are hidden
    // ***************************************

    $('#emailCheck').hide();
    $('#newpwdCheck').hide();
    $('#chkCheck').hide();
    $('#loginEmail').change(function () {
        validateEmail();
    });
    $('#loginPassword').change(function () {
        validatePwd();
    });
});

// ***************************************
// Email Validations
// ***************************************
function validateEmail() {
    var emailValue = $('#loginEmail').val();
    var mail_format = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
    if (mail_format.test(emailValue) && emailValue != '') {
        $('#emailCheck').hide();
        return true;
    }
    else {
        $('#emailCheck').show();
        $('#emailCheck').html("Please enter valid Email Address (Format - xyz@gmail.com)");
        return false;
    }
}

// ***************************************
// Password Validations
// ***************************************
function validatePwd() {
    if (($("#loginPassword").val() == '')) {
        $('#newpwdCheck').show();
        $('#newpwdCheck').html("Please enter valid Password");
        return false;
    }
    else {
        $('#newpwdCheck').hide();
        return true;
    }
}

// ***************************************
// License Validations (Check Box)
// ***************************************
function validateCheckBox() {
    if ($('input[id=acceptTs]:checked').length == 0) {
        $('#chkCheck').show();
        $('#chkCheck').html("**Please accept License agreement");
        return false;
    }
    else {
        $('#chkCheck').hide();
        return true;
    }
}

// ******************************************************************************
// If all validations are true than login is successfull
// ******************************************************************************
function validateData() {
    var valEmail = validateEmail()
    var valPass = validatePwd()
    var valCheckbox = validateCheckBox()


    if (valEmail && valPass && valCheckbox) {
        //alert("Login Successful");
        return Login();
    }
}

// ******************************************************************************
// Set cookie function
// ******************************************************************************
function setCookie() {
    if ($("#rememeberme").prop('checked') == true) {
        var email = $('#loginEmail').val()
        var password = $('#loginPassword').val()
        set_cookie('trackerflowemail', email);
        set_cookie('trackerflowpassword', password);
    }
    else {
        delete_cookie('trackerflowemail')
        delete_cookie('trackerflowpassword')
    }
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function set_cookie(name, value) {
    var now = new Date();
    now.setTime(now.getTime() + 1 * 3600 * 1000);
    //document.cookie = "expires=" + now.toUTCString() + ";"
    document.cookie = name + '=' + value + '; Path=/Sign/Login;; expires=' + now.toUTCString() + ';'
}
function delete_cookie(name) {
    document.cookie = name + '=; Path=/Sign/Login;; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}




// ***************************************
// AJAX 
// ***************************************
function Login() {
    var checked = false;
    if ($('#rememeberme').is(':checked')) { checked = true; }
    var UserClass = {
        //c_id: $('#txtid').val(),
        c_email: $('#loginEmail').val(),
        c_password: $('#loginPassword').val(),
        c_RememberMe: checked//$('#rememeberme').val()
    }
    console.log(UserClass);

    $.ajax({
        async: true,
        type: 'Post',
        url: API_URL + '/api/Register/Login',
        data: JSON.stringify(UserClass),
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != 0) { // id returned
                document.getElementById("successMessage").innerHTML = "Login Successful";
                document.getElementById("msgSuccess").style.display = "block";
                $("#suc").html("Please Wait...");
                $("#suc").prop("disabled", true);
                setTimeout(function () {
                    $("#msgSuccess").fadeOut(1000)
                    window.location = "/Sign/CheckSession/" + response;
                }, 500);
                setCookie();
                console.log(response);

                //window.location = "/Sign/Dashboard/" + response; // id from register table to fetch data into user profile
                console.log(response.data);
                ///
            }
            else {
                document.getElementById("warningMessage").innerHTML = "Either Email or Password is incorrect";
                document.getElementById("msgWarning").style.display = "block";
                setTimeout(function () {
                    $("#msgWarning").fadeOut(1000)
                }, 3000);
                console.log(response);

            }
        },
        error: function () {
            document.getElementById("errorMessage").innerHTML = "There is some error";
            document.getElementById("msgErrorr").style.display = "block";
            setTimeout(function () {
                $("#msgErrorr").fadeOut(1000)
            }, 3000);
            //window.location = "/Sign/Index";
        }
    });
}

function ExistEmail() {
    var ForgotByEmail = { ForgotByEmail: $('#txtemailsend').val() }

    console.log(ForgotByEmail);
    var mess = $("#username");

    var settings =
    {
        "url": API_URL + "/api/ForgotPasswordAPI",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(ForgotByEmail),
    };
    $.ajax(settings).done(function (response) {
        //console.log(response.c_id);  // if passed Bal.models.tblLogin();
        if (response != "") {

            ForgotByEmail = { ForgotByEmail: $('#txtemailsend').val(), fname: response } // setting response value i.e received firstname to variable fname
            var settings1 =
            {
                "url": API_URL + "/api/ForgotPasswordAPI/SendEmail",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify(ForgotByEmail),
            };
            $.ajax(settings1).done(function (response) {
                console.log(response);

                // if (response == 1)
                document.getElementById("successMessage").innerHTML = "Reset Password link is sent to the registered Email Id";
                document.getElementById("msgSuccess").style.display = "block";
                setTimeout(function () {
                    $("#msgSuccess").fadeOut(1000)
                    window.location = UI_URL + "/sign/login";
                }, 3000);

                //mess.html("Email Send...");
                //mess.css("color", "green");
            });
        } else {
            document.getElementById("warningMessage").innerHTML = "User does not exist ";
            document.getElementById("msgWarning").style.display = "block";
            setTimeout(function () {
                $("#msgWarning").fadeOut(1000)
            }, 3000);
            //alert('User not Exist');
            //mess.html("User Does Not Exist !");
            //mess.css("color", "red");
        }
    });
}