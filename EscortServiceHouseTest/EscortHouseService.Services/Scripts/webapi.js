var uri = "http://localhost:50825/api/account/register";

$(document).ready( function() {
    $('.profnav').hide();
});

var clearTheRegisterForm = function (username) {
    $('#reg').fadeOut();
    $('.navbar-form').hide();
    $('#navbar').hide();
    $('.profnav').fadeIn();
};

var manageError = function (xhr, status, error) {
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

function register() {
    var data = {
        "Username": $('input[name="Username"]').val(),
        "Email": $('input[name="Email"]').val(),
        "PhoneNumber": $('input[name="PhoneNumber"]').val(),
        "Gender": $('input[name="Gender"]:checked').val(),
        "Role": $('input[name="Role"]:checked').val(),
        "Password": $('input[name="Password"]').val(),
        "ConfirmPassword": $('input[name="ConfirmPassword"]').val()
    };

    $('.error').text(""); // Clear the errors for the input fields

    $.ajax({
        url: uri,
        type: 'POST',
        data: data,
        success: function () {
            clearTheRegisterForm();
            $('#regComplete').text('Registration completed successfully. Welcome ' + data['Username'] + '!');
            $('.btn-warning').removeClass('btn-warning').addClass('btn-success').text('Proceed to the gallery ->');
        },
        error: manageError
    });
};