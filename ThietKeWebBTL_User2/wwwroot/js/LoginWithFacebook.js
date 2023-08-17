//window.fbAsyncInit = function () {
//    FB.init({
//        appId: '188359867342676',
//        cookie: true,
//        xfbml: true,
//        version: 'v16.0'
//    });

//    // run during page load to check a person's login status
//    //FB.getLoginStatus(function (response) {
//    //    statusChangeCallback(response);
//    //});

//};

//(function (d, s, id) {
//    var js, fjs = d.getElementsByTagName(s)[0];
//    if (d.getElementById(id)) { return; }
//    js = d.createElement(s); js.id = id;
//    js.src = "https://connect.facebook.net/en_US/sdk.js";
//    fjs.parentNode.insertBefore(js, fjs);
//}(document, 'script', 'facebook-jssdk'));

//// connected - the person is logged into Facebook, and has logged into your app.
//function statusChangeCallback(response) {
//    if (response.status === 'connected') {
//        console.log("logged in and authenticated");
//        window.location.href = '/home/index';
//    }
//    else
//        console.log('Not authenticated');
//}

//function checkLoginState() {
//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//    });
//}

//function logout() {
//    FB.logout(function (response) {

//    });
//}


//  < !--=============================================================================================== -->

window.fbAsyncInit = function () {
    FB.init({
        appId: '188359867342676',
        cookie: true,
        xfbml: true,
        version: 'v16.0'
    });

    // run during page load to check a person's login status
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


$('#loginWithFacebook').off('click').on('click', function (e) {
    e.preventDefault();
    console.log("click");
    login();
});

function login() {
    FB.login(function (response) {
        if (response.status === 'connected') {
            // Logged into your webpage and Facebook.
            console.log("logged in and authenticated");
            testAPI();
            window.location.href = '/home/index';
            
        } else {
            // The person is not logged into your webpage or we are unable to tell. 
            console.log('Not authenticated');
        }
    }, { scope: 'public_profile,email,user_birthday, user_gender' });
}

function testAPI() {
    
    FB.api('/me?fields=name,email,birthday, picture, gender', function (response) {
        console.log(response);
        var email = String(response.email);
        var name = response.name;
        var strBirthday = response.birthday;
        var id = response.id;
        var gender = gender;
        $.ajax({
            url: '/Account/CreateAccount',
            data: {
                email: email,
                name: name,
                strBirthday: strBirthday
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                
            }
        });
        document.getElementById("accountImg").src = response.picture.data.url;
        //FB.api(
        //    '/' +id + '/picture',
        //    'GET',
        //    {},
        //    function (response) {
        //        console.log("picture");
        //        console.log(response);
        //    }
        //);
    });
}


function logout() {
    FB.logout(function (response) {

    });
}

function statusChangeCallback(response) {
    if (response.status === 'connected') {
        console.log("logged in and authenticated");
        testAPI();
    }
    else
        console.log('Not authenticated');
}

  

$('#shareFacebook').off('click').on('click', function (e) {
    e.preventDefault();
    console.log("click");
    FB.ui({
        method: 'send',
        display: 'popup',
        link: window.location.href,
    }, function (response) { });
});

//document.querySelector('.fb-like').setAttribute('data-href', window.location.href);



$(document).ready(function () {
    var autocomplete;
    autocomplete = new google.maps.places.Autocomplete((document.getElementById('addressInput')), {
        //types: ['geocode'],
        types: [`geocode`],
        /*componentRestrictions: {
         country: "USA"
        }*/
    });

    //google.maps.event.addListener(autocomplete, 'place_changed', function () {
    //    var near_place = autocomplete.getPlace();
    //});
});