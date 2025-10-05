
$(document).ready(function () {
    let userid = document.getElementById('userid').getAttribute('value');

    if (userid !== "" && userid !== "0") {
        //alert('Edit kka page');
        $('#submitBtn').text('Update');

    }
});


document.getElementById('registrationForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const userid = document.getElementById('userid').value.trim();
   // const userid = $('#userid').value.trim();
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    const gender = document.querySelector('input[name="gender"]:checked');
    const country = document.getElementById('country').value;
    const State = document.getElementById('State').value;
    const date = document.getElementById('date').value;
    const terms = document.getElementById('terms').checked;

    // Validations (as you've written)
    if (name === '') { alert("Please enter your full name!"); return; }
    if (!/^[a-zA-Z\s]+$/.test(name)) { alert("Name should contain only letters!"); return; }
    if (email === '') { alert("Please enter your email!"); return; }
    if (!email.includes('@') || !email.includes('.')) { alert("Enter a valid email address!"); return; }
    if (phone === '') { alert("Please enter your phone number!"); return; }
    if (!/^\d{10}$/.test(phone)) { alert("Phone number must be exactly 10 digits!"); return; }
    if (password === '') { alert("Please enter your password!"); return; }
    if (password.length < 6) { alert("Password must be at least 6 characters!"); return; }
    if (confirmPassword === '') { alert("Please confirm your password!"); return; }
    if (password !== confirmPassword) { alert("Passwords do not match!"); return; }
    if (!gender) { alert("Please select your gender!"); return; }
    if (country === '') { alert("Please select your country!"); return; }
    if (date === '') { alert("please select the date correctly!"); return; }
    if (!terms) { alert("You must agree to the Terms & Conditions!"); return; }

    // Prepare data
    const formData = {
        userid: userid,
        Name: name,
        Email: email,
        Phone: phone,
        Password: password,
        ConfirmPassword: confirmPassword,
        Gender: gender.value,
        Country: country,
        State: State,
        date: date,
        TermsAccepted: terms
    };
  /.
    else {
        $.ajax({
            debugger
            url: '/Form/update', // Adjust to your actual controller/action
            type: 'POST',
            data: formData,
            success: function (data) {
                $('#result').html(data.message || "Update successful!");
                alert("Updates Successful!");
                document.getElementById('registrationForm').reset();

                window.location.href = '/Form/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
});


