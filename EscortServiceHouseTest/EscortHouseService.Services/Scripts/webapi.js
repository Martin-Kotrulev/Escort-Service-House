var escortRegisterUrl = "http://localhost:50825/api/account/register/escort";
var customerRegisterUrl = "http://localhost:50825/api/account/register/customer";
var loginUrl = "http://localhost:50825/Token";
var profPicDefaultPath = "Content/unknown.png";
var cookieDeleteDate = "01 Jan 1970 00:00:00 UTC";
var userInfoUrl = "http://localhost:50825/api/account/UserInfo";
var guestEscortUrl = "http://localhost:50825/api/guest/escorts";
var guestEscortCountUrl = "http://localhost:50825/api/guest/escorts/count";
var guestEscortDetailInfo = "http://localhost:50825/api/guest/escorts/";

$(document).ready(function () {
    if (document.cookie) {
        setLoggedView();
    } else {
        setForRegister();
    }
});

function clearCookie() {
    document.cookie = 'username=; expires=' + cookieDeleteDate;
    document.cookie = 'access_token=; expires=' + cookieDeleteDate;
    document.cookie = 'token_type=; expires=' + cookieDeleteDate;
}

function showEscortProfile() {
    console.log("escort profile");
}

function guestEscortInfo(element) {
   
    $.ajax({
        url: guestEscortDetailInfo + element.name,
        type: "GET",
        success: function (data, text, xhr) {
            var json = JSON.parse(xhr.responseText),
                name = json['UserName'],
                pic_src = json['B64Profile'],
                desc = json['Description'],
                price = json['HourRate'],
                town = json['Town'];
            $('#myModal').modal();
            $('#myModal').on('shown.bs.modal', function () {
                $("#modal_pic").attr('src', pic_src);
                $('#mod_name').text(name);
                $('#mod_desc').text(desc);
                $('#mod_price').text(price + '$');
                $('#mod_town').text(town);
            });
        }
    });
}

function showPaginationResults(element) {
    $('#login_error').text('');
    var url = element.name;
    $.ajax({
        url: url,
        type: "GET",
        success: function (data, text, xhr) {
            $('#escorts').empty();
            var usersArr = JSON.parse(xhr.responseText);
            for (var i in usersArr) {
                var picture = usersArr[i]['B64Profile'];
                if (picture) {
                    $('#escorts').append('<li class="col-lg-3 col-md-4 col-sm-6 col-xs-6"><img src="'
                        + picture
                        + '" name="' + usersArr[i]['UserName'] + '" class="image-responsive" onclick="guestEscortInfo(this)"></li>');
                }
            }
        }
    });
    $('.pagination>li[class="active"]').removeClass('active');
    $('#' + element.id).parent().addClass('active');
}

function addPaginationSchema() {
    $.ajax({
        url: guestEscortCountUrl,
        type: "GET",
        success: function (data, text, xhr) {
            var pages = Math.round(xhr.responseText / 8);
            var skip = 0;
            var top = 8;
            if (pages > 1) {
                for (var i = 0; i < pages; i++) {
                    $('.pagination').append('<li id="page_'
                        + i + '"><a href="#" id="href_' + i + '" name="'
                        + guestEscortUrl + '?$top=' + top + '&$skip='
                        + skip + '" onclick="showPaginationResults(this)">' + (i + 1) + '</a></li>');
                    skip += top;
                }
                $('#page_0').addClass('active');
                $('.pagination').show();
            }
        }
    });
}

function showGuestGallery() {
    $.ajax({
        url: guestEscortUrl + '?$top=8',
        type: "GET",
        success: function (data, text, xhr) {
            var usersArr = JSON.parse(xhr.responseText);
            for (var i in usersArr) {
                var picture = usersArr[i]['B64Profile'];
                if (picture) {
                    $('#escorts').append('<li class="col-lg-3 col-md-4 col-sm-6 col-xs-6"><img src="'
                        + picture + '" name="' + usersArr[i]['UserName'] + '" class="image-responsive" onclick="guestEscortInfo(this)"></li>');
                }
            }
        }
    });
}

function enterGuestMode() {
    $('#reg').hide();
    clearLogoAndProceedButton();
    addPaginationSchema();
    showGuestGallery();
}

function setLoggedView() {
    $('#login_error').text('');
    var cookies = document.cookie.split('; '),
        token = cookies[1].split('=')[1],
        type = cookies[2].split('=')[1],
        user = cookies[0].split('=')[1];

    $.ajax({
        url: userInfoUrl,
        type: "GET",
        dataType: 'jsonp',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", 'Bearer ' + token);
        },
        complete: function (xhr) {
            var infoJson = JSON.parse(xhr.responseText);
            if (infoJson['Roles'][0] === 'Escort') {
                $('.escort-btn').show();
                $('#home_btn').click(showEscortProfile);
            } else {
                $('.escort-btn').hide();
            }
        }
    });

    $('#home_btn').text(user);
    clearRegisterPanel();
    clearLogoAndProceedButton();
}

function clearLogoAndProceedButton() {
    $('.jcont').hide();
    $('.guest').hide();
}

function login() {
    emptySearch();
    // Getting the user name and password from the login form
    // or when registering from the register from
    var user_name = $('#usr_name').val(),
        user_password = $('#usr_pass').val();
    if (!user_name || !user_password) {
        user_name = $('input[name="Username"]').val();
        user_password = $('input[name="Password"]').val();
    }

    var token = {
            "username": user_name,
            "password": user_password,
            "grant_type": "password"
        };

    $.ajax({
        url: loginUrl,
        type: "POST",
        data: token,
        success: function (data, textStatus, xhr) {
            var responseJson = JSON.parse(xhr.responseText),
                access_token = responseJson['access_token'],
                token_type = responseJson['token_type'],
                user_name = responseJson['userName'],
                expires = responseJson['.expires'];

            document.cookie = 'username=' + user_name + '; expires=' + expires;
            document.cookie = 'access_token=' + access_token + '; expires=' + expires;
            document.cookie = 'token_type=' + token_type + '; expires=' + expires;

            $('.profnav').fadeIn();
            setLoggedView();
        },
        error: function (xhr, status, error) {
            var errJson = JSON.parse(xhr.responseText),
                errMsg = errJson['error_description'];
            $('#login_error').text(errMsg);
            $('#usr_name').val('');
            $('#usr_pass').val('');
        }
    });
};

function emptySearch() {
    $('#escorts').empty();
    $('.pagination').empty();
}

function logout() {
    clearCookie();
    setForRegister();
}

function managePicture() {
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    if (file) {
        reader.readAsDataURL(file);
    }

    reader.onloadend = function () {
        $('#profPic').attr('src', reader.result);
    }
}

function setForRegister() {
    $('input[type=text]').val('');
    $('textarea').val("");
    $('input[type=number]').val('');
    $('input[type=password').val('');
    $('.profnav').hide();
    $('.escort-extra').hide();
    $('.navbar-form').show();
    $('#reg').fadeIn();
    $('#profPic').attr('src', profPicDefaultPath);
    $('#B64').replaceWith($('#B64').val('').clone(true));
    $('input[type=radio]').removeAttr('checked');
    $('option').removeAttr('selected');
    $('input[value=Customer]').prop('checked', true);
    $('input[value=Male]').prop('checked', true);
    $('option[value=Natural]').prop('selected', true);
    $('option[value=Sofia]').prop('selected', true);
    $('#regComplete').text('');
    $('.guest>button').removeClass('btn-success').addClass('btn-warning').text('Continue as Guest ->');
    $('#login_error').text('');
    $('.jcont').fadeIn();
    $('.guest').fadeIn();
    emptySearch();
}

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
    $('#reg').hide();
    $('.navbar-form').hide();
    $('#regComplete').text('');
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

function showProceedBtn() {
    $('.guest>button').removeClass('btn-warning').addClass('btn-success').text('Proceed to the gallery ->').click(login);
}

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
            showProceedBtn();
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
        "Town": $('select[name="Town"]').val(),
        "Height": $('input[name="Height"]').val(),
        "Weight": $('input[name="Weight"]').val(),
        "BreastsType": $('select[name="BreastsType"]').val(),
        "BreastsSize": $('input[name="BreastsSize"]').val(),
        "HairColor": $('input[name="HairColor"]').val(),
        "Description": $('textarea[name="Description"]').val(),
        "B64": $('#profPic').attr('src')
    };

    $.ajax({
        url: escortRegisterUrl,
        type: 'POST',
        data: escort,
        success: function (role) {
            clearRegisterPanel();
            $('#regComplete').text('Registration completed successfully. Welcome ' + escort['Username'] + '!');
            showProceedBtn()
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
