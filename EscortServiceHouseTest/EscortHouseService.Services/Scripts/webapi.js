var escortRegisterUrl = "http://redbarroness.azurewebsites.net/api/account/register/escort";
var customerRegisterUrl = "http://redbarroness.azurewebsites.net/api/account/register/customer";
var loginUrl = "http://redbarroness.azurewebsites.net/Token";
var profPicDefaultPath = 'data:image/png;base64,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';

var cookieDeleteDate = "01 Jan 1970 00:00:00 UTC";
var userInfoUrl = "http://redbarroness.azurewebsites.net/api/account/UserInfo";
var guestEscortUrl = "http://redbarroness.azurewebsites.net/api/guest/escorts";
var guestEscortCountUrl = "http://redbarroness.azurewebsites.net/api/guest/escorts/count";
var guestEscortDetailInfo = "http://redbarroness.azurewebsites.net/api/guest/escorts/";
var escortProfilePictureUrl = "http://redbarroness.azurewebsites.net/api/escort/pictures/profile";
var escortNonProfilePicturesUrl = "http://redbarroness.azurewebsites.net/api/escort/pictures/nonprofile";
var escortAllPicturesUrl = "http://redbarroness.azurewebsites.net/api/escort/pictures";
var escortPicturesAdd = "http://redbarroness.azurewebsites.net/api/escort/pictures/add";
var escortChangeProfPic = "http://redbarroness.azurewebsites.net/api/escort/pictures/";

$(document).ready(function () {
    $('img').attr('src', profPicDefaultPath);
    if (document.cookie.indexOf("username") >= 0) {
        setLoggedView();
    } else {
        setForRegister();
    }
});

function manageNewPics(element) {
    var id = $(element).siblings()[0].id;
    $("#add_delete_pics_modal #" + id).siblings()[0].disabled = false;

    var file = element.files[0];
    var reader = new FileReader();

    if (file) {
        reader.readAsDataURL(file);
    }

    reader.onloadend = function () {
        $('#add_delete_pics_modal #' + id + ' img').removeAttr('src');
        $('#add_delete_pics_modal #' + id + ' img').attr('src', reader.result);
    }
}

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
            $('#guest_modal').modal();
            $('#guest_modal').on('shown.bs.modal', function () {
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

function setTheEscortView() {
    var cookies = document.cookie.split('; '),
        token = cookies[2].split('=')[1];

    $.ajax({
        url: escortProfilePictureUrl,
        type: "GET",
        dataType: "jsonp",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        complete: function (xhr) {
            var infoJson = JSON.parse(xhr.responseText),
                id = infoJson["Id"],
                B64 = infoJson["B64"]
            $("#prof_pic>img").attr("src", B64).attr("id", id);
        }
    });

    $.ajax({
        url: escortAllPicturesUrl,
        type: "GET",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        complete: function (xhr) {
            var modalFields = 6,
                allPictures = JSON.parse(xhr.responseText),
                len = allPictures.length;

            for (var i = 0; i < modalFields; i++) {
                if (i < len) {
                    $("#pic_pan #pic" + i + " img" + ", #add_delete_pics_modal #pic" + i + " img")
                        .attr("src", allPictures[i]["B64"])
                        .attr("id", allPictures[i]["Id"]);
                    $("#add_delete_pics_modal #pic" + i).siblings()[0].disabled = true;
                    $("#add_delete_pics_modal #pic" + i).siblings()[1].disabled = false;
                    $("#add_delete_pics_modal #pic" + i).siblings()[2].disabled = true;
                    console.log("picture applied");
                } else {
                    console.log("empty field hidden");
                    $("#add_delete_pics_modal #pic" + i).siblings()[0].disabled = true;
                }
            }
        }
    });
    $('.user-panel').show();
}

function setProfilePic(element) {
    var confirmResult = confirm("Are you sure you want to change your profile picture?");
    if (confirmResult) {
        var elementId = $(element).siblings()[0].id;
        var picId = $('#' + elementId + " img").attr("id");
        var cookies = document.cookie.split('; '),
            token = cookies[2].split('=')[1];
        console.log(picId);
        $.ajax({
            url: escortChangeProfPic + picId + '/change',
            type: "PUT",
            beforeSend: function(xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function () {
                setTheEscortView();
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            }
        });
    }
}

function delPic(element) {
    var confirmResult = confirm("Are you sure you want to delete this picture?");
    if (confirmResult) {
        var elementId = $(element).siblings()[0].id;
        var picId = $('#' + elementId + " img")[1].id;
        var cookies = document.cookie.split('; '),
            token = cookies[2].split('=')[1];
        console.log(picId);
        
        console.log(picId);
        $.ajax({
            url: escortChangeProfPic + picId + '/delete',
            type: "DELETE",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function () {
                location.reload();
            },
            error: function (xhr) {
                var responseJson = JSON.parse(xhr.responseText);
                alert(responseJson["Message"]);
            }
        });
        
    }
}
function editPictures() {
    $("#add_delete_pics_modal").modal();
}

function addPic(element) {
    var confirmResult = confirm("Are you sure you want to add this picture?");
    if (confirmResult) {
        var pictureDiv = $(element).siblings()[0],
            pictureElement = $("#" + pictureDiv.id + " img"),
            picture = { "B64": pictureElement[1].src },
            cookies = document.cookie.split('; '),
            token = cookies[2].split('=')[1];
        $.ajax({
            url: escortPicturesAdd,
            type: "POST",
            data: picture,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (xhr) {
                location.reload();
            },
            error: function (xhr) {
                var responseJson = JSON.parse(xhr.responseText);
                console.log(responseJson["Message"]);
            }
        });
    }
}

function changeProfilePic() {
    $("#profile_pic_modal>.container").show();
    var id = $("#prof_pic>img").attr("id");

    $('#profile_pic_modal').modal();
    var cookies = document.cookie.split('; '),
            token = cookies[2].split('=')[1];
    
    $.ajax({
        url: escortNonProfilePicturesUrl,
        type: "GET",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        complete: function (xhr) {
            var nonProfilePictures = JSON.parse(xhr.responseText),
                len = nonProfilePictures.length,
                modalFields = 5;
            for (var i = 0; i < modalFields; i++) {
                if (i < len) {
                    $("#profile_pic_modal #pic" + i + " img")
                        .attr("src", nonProfilePictures[i]["B64"])
                        .attr("id", nonProfilePictures[i]["Id"]);
                } else {
                    $("#profile_pic_modal #pic" + i).parent().hide();
                }
            }
            if (len == 0) {
                $("#ch_profile_pic_err").text("There are no additional pictures to change!");
            }
        }
    });
    $('#profile_pic_modal').on('hide.bs.modal', function () {
        $("#ch_profile_pic_err").text("");
    });
}

function setLoggedView() {
    $('#login_error').text('');
    var cookies = document.cookie.split('; '),
        token = cookies[2].split('=')[1],
        type = cookies[3].split('=')[1],
        user = cookies[1].split('=')[1];

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
                //$('.escort-btn').show();
                $('.only-customer').hide();
                setTheEscortView();
                $('#home_btn').click(showEscortProfile);
            } else {
                //$('.escort-btn').hide();
                $('.only-escort').hide();
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
    location.reload();
    //setForRegister();
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
    $('.user-panel').hide();
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
    $('.guest>button').removeClass('btn-warning').addClass('btn-success').text('Proceed to the gallery ->').prop('onclick', null).click(login);
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
            showProceedBtn();
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
