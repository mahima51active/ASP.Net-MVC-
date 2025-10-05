//$(document).ready(function () {
//    $("#btnSubmit").click(function () {
//        validateForm();
//    });
//    var selectedCountry = "@country";
//    var selectedState = "@state";
//    var selectedCity = "@city";

//    if (selectedCountry !== "") {
//        $('#country').val(selectedCountry);
//        getStates(selectedCountry, selectedState, selectedCity); // load states and cities
//    }
//});
 function getStates(selectedCountry, selectedState='', selectedCity='') {
        var countryId = $('#country').val();

        $.ajax({
            url: '/Home/GetStates',
            type: 'GET',
            data: { countryId: countryId },
            //success: function (states) {
            //    console.log(states);
            //    $('#state').empty().append('<option value="">--Select State--</option>');
            //    $.each(states, function (i, state) {
            //        $('#state').append('<option value="' + state.Value + '">' + state.Text + '</option>');
            //    });
            //    $('#city').empty().append('<option value="">--Select City--</option>');
            //}
            success: function (states) {
                $('#state').empty().append('<option value="">-- Select State --</option>');
                $.each(states, function (i, state) {
                    var selected = (state.Value == selectedState) ? 'selected' : '';
                    $('#state').append(`<option value="${state.Value}" ${selected}>${state.Text}</option>`);
                });

                if (selectedState !== '') {
                    getCities(selectedState, selectedCity); // load cities after states loaded
                }
            }
        });
    }

function getCities(stateId, selectedCity = "") {
         var stateId = $('#state').val();

    $.ajax({
        url: '/Home/GetCities',
        type: 'GET',
        data: { stateId: stateId },
        success: function (cities) {
            $('#city').empty().append('<option value="">--Select City--</option>');
            $.each(cities, function (i, city) {
                let selected = (city.Value === selectedCity) ? "selected" : "";
                $('#city').append(`<option value="${city.Value}" ${selected}>${city.Text}</option>`);
            });
        }
    });
}
//    function getCities() {
//        var stateId = $('#state').val();

//        $.ajax({
//            url: '/Home/GetCities',
//            type: 'GET',
//            data: { stateId: stateId },
//            success: function (cities) {
//                $('#city').empty().append('<option value="">--Select City--</option>');
//                $.each(cities, function (i, city) {
//                    $('#city').append('<option value="' + city.Value + '">' + city.Text + '</option>');
//                });
//            }
//        });
//}
function validateForm() {
    let userid = document.getElementById('userid').value;
    let txtname = document.getElementById('fullname').value;
    let txtemail = document.getElementById('email').value;
    let txtphone = document.getElementById('phone').value;
    let txtpassword = document.getElementById('password').value;
    //let txtgender = document.getElementById('gender').value;
    let txtgender = document.querySelector('input[name="gender"]:checked');

    let txtcountry = document.getElementById('country').value;
    let txtstate = document.getElementById('state').value;
    let txtcity = document.getElementById('city').value;
    if (txtname == null || txtname == undefined) { alert("please enter your name"); return; }
    if (txtemail == null || txtemail == undefined) { alert("please enter your email"); return; }
    if (txtphone == null || txtphone == undefined) { alert("please enter your phone"); return; }
    if (txtpassword == null || txtpassword == undefined) { alert("please enter your password"); return; }
    if (txtgender == null || txtgender == undefined) { alert("please select your gender"); return; }
    if (txtcountry == null || txtcountry == undefined) { alert("please select your country"); return; }
    if (txtstate == null || txtstate == undefined) { alert("please select your state"); return; }
    if (txtcity == null || txtcity == undefined) { alert("please select your city"); return; }
    dataString = new FormData($("#frmRegistration").get(0));
    //

    if (dataString != '') {
        
        $.ajax({
            type: "POST",
            url: "/Home/About",
            data: dataString,
            contentType: false,
            processData: false,
            success: function (response) {
                var res = response.split('-');
                if (res[0] == "True") {
                        window.location.href = "/Home/Index";
                    return true;.


                }
                else {
                    return false;
                }
            },
            error: function (er) {
                return false;
            }
        });
    }
}

$("#txtimage").change(function () {
    debugger;
    ReadsPhotoImg(this);
});
function ReadsPhotoImg(input) {
    if (input.files && input.files[0]) {
        var readers = new FileReader();
        readers.onload = function (e) {
            $('#Photo-preview').attr('src', e.target.result);
        }
        readers.readAsDataURL(input.files[0]);
    }
}

function ReadsPhotoImg(input) {
    if (input.files && input.files[0]) {
        var readers = new FileReader();
        readers.onload = function (e) {
            $('#Photo-preview').attr('src', e.target.result);
        }
        readers.readAsDataURL(input.files[0]);
    }
}


   





