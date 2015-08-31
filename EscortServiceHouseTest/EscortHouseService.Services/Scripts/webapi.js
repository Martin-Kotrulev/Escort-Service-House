var escortRegisterUrl = "http://localhost:50825/api/account/register/escort";
var customerRegisterUrl = "http://localhost:50825/api/account/register/customer";

$(document).ready( function() {
    $('.profnav').hide();
    $('.escort-extra').hide();
});

function changeForm() {
    $('.error').text("");
    var role = $('input[name="Role"]:checked').val();
    if (role === "Escort") {
        $('.escort-extra').fadeIn();
    } else if (role === "Customer") {
        $('.escort-extra').fadeOut();
    }
}

var clearRegisterPanel = function () {
    $('#reg').fadeOut();
    $('.navbar-form').hide();
    $('#navbar').hide();
    $('.profnav').fadeIn();
};

var manageErrorJson = function (xhr, status, error) {
    var response = JSON.parse(xhr.responseText),
        ms = response["ModelState"];
    for (var state in ms) {
        if (state) {
            var tag = state.split('.')[1],
            txt = ms[state];
            $('#' + tag + '~.error').text(" * " + txt);
        } else {
            // These types of error are only for the username and email
            for (var i = 0; i < ms[state].length; i++) {
                var message = ms[state][i];
                if (message.split(' ')[0] === 'Name') {
                    $('#Username~.error').text(message);
                } else {
                    $('#Email~.error').text(message);
                }
            }
        }
    }
};

function registerCustomer() {
    var customer = {
        "Username": $('input[name="Username"]').val(),
        "Email": $('input[name="Email"]').val(),
        "PhoneNumber": $('input[name="PhoneNumber"]').val(),
        "Gender": $('input[name="Gender"]:checked').val(),
        "Password": $('input[name="Password"]').val(),
        "ConfirmPassword": $('input[name="ConfirmPassword"]').val()
    };

    $('.error').text(""); // Clear the errors for the input fields

    $.ajax({
        url: customerRegisterUrl,
        type: 'POST',
        data: customer,
        success: function () {
            clearRegisterPanel();
            $('#regComplete').text('Registration completed successfully. Welcome ' + customer['Username'] + '!');
            $('.btn-warning').removeClass('btn-warning').addClass('btn-success').text('Proceed to the gallery ->');
        },
        error: manageErrorJson
    });
}

function registerEscort() {
    var escort = {
        "Username": $('input[name="Username"]').val(),
        "Email": $('input[name="Email"]').val(),
        "PhoneNumber": $('input[name="PhoneNumber"]').val(),
        "Gender": $('input[name="Gender"]:checked').val(),
        "Password": $('input[name="Password"]').val(),
        "ConfirmPassword": $('input[name="ConfirmPassword"]').val(),
        "Town": $('input[name="Town"]').val(),
        "Height": $('input[name="Height"]').val(),
        "Weight": $('input[name="Weight"]').val(),
        "BreastType": $('input[name="BreastType"]').val(),
        "BreastSize": $('input[name="BreastSize"]').val(),
        "HairColor": $('input[name="HairColor"]').val(),
        "Description": $('textarea[name="Description"]').val()
    };

    $.ajax({
        url: escortRegisterUrl,
        type: 'POST',
        data: escort,
        success: function () {
            clearTheRegisterForm();
            $('#regComplete').text('Registration completed successfully. Welcome ' + escort['Username'] + '!');
            $('.btn-warning').removeClass('btn-warning').addClass('btn-success').text('Proceed to the gallery ->');
        },
        error: manageErrorJson
    });
}

function register() {
    var status = $('input[name="Role"]:checked').val();
    if (status === "Escort") {
        registerEscort();
    } else if (status == "Customer") {
        registerCustomer();
    }
}

