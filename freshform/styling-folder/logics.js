
//$(document).ready(function () {
//    document.getElementById('pribtn').addEventListener('submit', function (e) {
//        e.preventDefault();
//        debugger;
//        alert("heloo this is my event")


//    });
//});


document.getElementById('click').addEventListener('click', function (e) {
    e.preventDefault();
    debugger;
    alert("heloo this is my event");
    let firstname   = document.getElementById('firstName').value;
    let lastname    = document.getElementById('lastName').value;
    let dob         = document.getElementById('dob').value;
    let gender      = document.querySelector('input[name="gender"]:checked').value;//document.getElementById('gender').value;
    let adhar       = document.getElementById('adhar').value;
    let phone       = document.getElementById('phone').value;
    let email       = document.getElementById('email').value;
    let address     = document.getElementById('address').value;
    let city        = document.getElementById('city').value;
    let state       = document.getElementById('state').value;
    let zip         = document.getElementById('zip').value;
    let ssc         = document.getElementById('ssc').value;
    let hsc         = document.getElementById('hsc').value;
    let ug          = document.getElementById('ug').value;
    let pg          = document.getElementById('pg').value;
    let course      = document.getElementById('course').value;
    let session     = document.getElementById('session').value;
    // ✅ Email Regex Pattern

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,4}$/;


    if(firstname== '' || firstname == null ){alert('Please Enter Firstname');return;}
    if(lastname == '' || lastname  == null ){alert('Please Enter Lastname ');return;}
    if(dob      == '' || dob       == null ){alert('Please Enter Dob      ');return;}
    if(gender   == '' || gender    == null ){alert('Please Enter Gender   ');return;}
    if(adhar    == '' || adhar     == null ){alert('Please Enter Adhar    ');return;}
    if(phone    == '' || phone     == null ){alert('Please Enter Phone    ');return;}
    if (email   == '' || email     == null) { alert('Please Enter Email   '); return; }
    if (!emailPattern.test(email))          { alert("Please enter a valid email address!"); return; }
    if(address  == '' || address   == null ){alert('Please Enter Address  ');return;}
    if(city     == '' || city      == null ){alert('Please Enter City     ');return;}
    if(state    == '' || state     == null ){alert('Please Enter State    ');return;}
    if(zip      == '' || zip       == null ){alert('Please Enter Zip      ');return;}
    if(ssc      == '' || ssc       == null ){alert('Please Enter Ssc      ');return;}
    if(hsc      == '' || hsc       == null ){alert('Please Enter Hsc      ');return;}
    if(ug       == '' || ug        == null ){alert('Please Enter Ug       ');return;}
    if(pg       == '' || pg        == null ){alert('Please Enter Pg       ');return;}
    if(course   == '' || course    == null ){alert('Please Enter Course   ');return;}
    if (session == '' || session == null) { alert('Please  Enter Session  '); return; }

    let formdata = {
        firstname: firstname,
        lastname: lastname,
        dob: dob,
        gender: gender,
        adhar: adhar,
        phone: phone,
        email: email,
        address: address,
        city: city,
        state: state,
        zip: zip,
        ssc: ssc,
        hsc: hsc,
        ug: ug,
        pg: pg,
        course: course,
        session: session,
    }
    console.log(formdata);
    debugger;
    $.ajax({
        url: '/form/Insert',  
        type: 'POST', 
        data: formdata,
        success: function (data) {
            $('#result').html(data.message || "Registration successful!");
            alert("Registration Successful!");
            //document.getElementById('registrationForm').reset(); 
            window.location.href = '/Form/listing';
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    });


























});