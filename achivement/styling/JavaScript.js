$(document).ready(function () {
    let userid = document.getElementById('userid').getAttribute('value');

    if (userid !== "" && userid !== "0") {
        //alert('Edit kka page');
        $('#click').text('Update');

    }
});

document.getElementById('click').addEventListener('click', function (e) {
    e.preventDefault();
    debugger;
    let fullname = document.getElementById('name').value;
    let email = document.getElementById('email').value;
    let phone = document.getElementById('phone').value;
    //let gender =document.getElementById('gender').value;
    let dob = document.getElementById('dob').value;
    let address = document.getElementById('address').value;
    let state = document.getElementById('state').value;
    let city = document.getElementById('city').value;
    let pin = document.getElementById('pin').value;
    let userid = document.getElementById('userid').getAttribute('value');

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,4}$/;

    if (fullname == '' || fullname == null) { alert("please enter your fullname correctly"); return; }
    if (email == '' || email == null) { alert("please enter your email correctly"); return; }
    if (!emailPattern.test(email)) { alert("Please enter a valid email address!"); return; }
    if (phone == '' || phone == null) { alert("please enter your phone correctly"); return; }
    if (dob == '' || dob == null) { alert("please enter your dob correctly"); return; }
    if (address == '' || address == null) { alert("please enter your address correctly"); return; }
    if (state == '' || state == null) { alert("please enter your state correctly"); return; }
    if (city == '' || city == null) { alert("please enter your city correctly"); return; }
    if (pin == '' || pin == null) { alert("please enter your pin correctly"); return; }

    let details = {
        userid: userid,
        fullname: fullname,
        email: email,
        phone: phone,
        dob: dob,
        address: address,
        state: state,
        city: city,
        pin: pin
    }
    console.log(details);
    if (userid !== "" && userid !== "0") {
        $.ajax({
            url: '/Default/update',
            type: 'POST',
            data: details,
            success: function (data) {
                $('#result').html(data.message || "Registration successful!");
                alert("Registration update successfully!");
                //document.getElementById('registrationForm').reset(); 
                window.location.href = '/Default/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    } else {
        $.ajax({
            url: '/Default/add',
            type: 'POST',
            data: details,
            success: function (data) {
                $('#result').html(data.message || "Registration successful!");
                alert("Registration Successful!");
                //document.getElementById('registrationForm').reset(); 
                window.location.href = '/Default/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });

    }

});
