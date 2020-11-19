/// <reference path="jquery-3.3.1.min.js" />

function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accesstoken = location.hash.split('access_token=')[1].split('&')[0];
            if (accesstoken) {
                isUserRegistered(accesstoken);
            }
        }
    }
}

function isUserRegistered(accesstoken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        contentType: 'application/json',
        headers:
        {
            'Authorization': 'bearer ' + accesstoken
        },
       success: function (response) {
           if (response.HasRegistered) {
               localStorage.setItem("accessToken", accesstoken);
               localStorage.setItem("userName", response.Email);
               window.location.href = "Data.html";
           }
           else {
               signUpExternalUser(accesstoken, response.LoginProvider);
           }
         }
    });
}


function signUpExternalUser(accesstoken,Provider) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        contentType: 'application/json',
        headers:
        {
            'Authorization': 'bearer ' + accesstoken
        },
        success: function () {
            window.location.href = "/api/Account/ExternalLogin?provider=" + Provider+"&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A53722%2FLogin.html&state=_vlLBKiCXsFXHo7xId0ep8j9jOk11KKvSGkw7A0Q_Z81";
        }
    });
}