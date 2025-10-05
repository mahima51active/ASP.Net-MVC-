$(document).ready(function () {
    let frm_id = document.getElementById('userid').getAttribute('value');

    if (frm_id !== "" && frm_id !== "0") {
        //alert('Edit kka page');
        $('#click').text('Update');

    }
});

document.getElementById('click').addEventListener('click', function (e) {
    e.preventDefault();
    alert("hello please submit these form!");

    let fullname = document.getElementById('fullname').value;
    let email = document.getElementById('email').value;
    let phone = document.getElementById('phone').value;
    let gender = document.getElementById('gender').value;
    let dob = document.getElementById('dob').value;
    let address = document.getElementById('address').value;
    let state = document.getElementById('state').value;
    let city = document.getElementById('city').value;
    let pincode = document.getElementById('pincode').value;

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2, 4}$/;

    if (fullname == "" && fullname == null) { alert("please enter fullname correctly!"); return; }
    if (email == "" && email == null) { alert("please enter email correctly!"); return; }
    //if (!emailPattern.test(email)) {alert("Please enter a valid email address!"); return; }
    if (phone == "" && phone == null) { alert("please enter phone correctly!"); return; }
    if (dob == "" && dob == null) { alert("please enter dob correctly!"); return; }
    if (address == "" && address == null) { alert("please enter address correctly!"); return; }
    if (state == "" && state == null) { alert("please enter state correctly!"); return; }
    if (city == "" && city == null) { alert("please enter city correctly!"); return; }
    if (pincode == "" && pincode == null) { alert("please enter pincode correctly!"); return; }


    let formdata = {
        userid: userid,
        fullname: fullname,
        email: email,
        phone: phone,
        gender: gender,
        dob: dob,
        address: address,
        state: state,
        city: city,
        pincode: pincode,
    }
    console.log(formdata);
    debugger;
    if (userid == "0" || userid == "") {
    $.ajax({
        url: '/Default/Insert',
        type: 'POST',
        data: formdata,
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
      else {
        $.ajax({
            url: '/Default/update', // Adjust to your actual controller/action
            type: 'POST',
            data: formdata,
            success: function (data) {
                $('#result').html(data.message || "Update successful!");
                alert("Updates Successful!");
                document.getElementById('registrationForm').reset();

                window.location.href = '/Default/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
    });