document.getElementById('loginform').addEventListener('Register', function (e) {
    e.preventDefault();

    //declare variables for getting data into the variables
    const userid = document.getElementById('userid').value.trim();
    const fullname = document.getElementById('fullname').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const address = document.getElementById('address').value.trim();
    const city = document.getElementById('city').value.trim();
    const state = document.getElementById('state').value.trim();

    //validations
    if (userid === "") { alert("please fill the form correctly"); return; }
    if (fullname === "") { alert("please Enter your fullname correctly"); return; }
    if (email === "") { alert("please Enter your email correctly"); return; }
    if (!email.includes('@') || !email.includes('.')) { alert("Enter a valid email address!"); return; }
    if (phone === "") { alert("please enter your phone numner correctly"); return; }
    if (address === "") { alert("please enteer your address correctly"); return; }
    if (city === "") { alert("please enter your city correctly"); return; }
    if (state === "") { alert("please enter your state correctly"); return; }

    const formdata = {
        userid = userid,
        fullname = fullname,
        email = email,
        phone = phone,
        address = address,
        city = city,
        state = state
    };


    $.ajax({
        debugger;
        url: '/great/insert',
        type: 'POST',
        data: formdata, // Sending as application/x-www-form-urlencoded
        success: function (data) {
            $('#result').html(data.message || "Registration successful!");
            Swal.fire({
                title: "Registration",
                text: "Successful!!",
                icon: "success"
            });

            //window.location.href = "/abc/dashboard";
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    });

}

