 
document.getElementById('RegistrationForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const name = document.getElementById('fullname').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const country = document.getElementById('country').value;
    const state = document.getElementById('state').value;
    const city = document.getElementById('city').value;
    const genderElement = document.querySelector('input[name="gender"]:checked');

    if (name === '') { Swal.fire({ title: "Warning", text: "Please enter your full name!", icon: "warning" }); return; }
    if (email === '') { Swal.fire({ title: "Warning", text: "Please enter your email!", icon: "warning" }); return; }
    if (!email.includes('@') || !email.includes('.')) { Swal.fire("Enter a valid email address!"); return; }
    if (phone === '') { Swal.fire({ title: "Warning", text: "Please enter your phone number!", icon: "warning" }); return; }
    if (!/^\d{10}$/.test(phone)) { Swal.fire("Phone number must be exactly 10 digits!"); return; }
    if (country === '') { Swal.fire({ title: "Warning", text: "Please select your country!", icon: "warning" }); return; }
    if (state === '') { Swal.fire({ title: "Warning", text: "Please enter your state!", icon: "warning" }); return; }
    if (city === '') { Swal.fire({ title: "Warning", text: "Please enter your city!", icon: "warning" }); return; }
    if (!genderElement) { Swal.fire({ title: "Warning", text: "Please select your gender!", icon: "warning" }); return; }
    

    const gender = genderElement.value;

    const formData = {
        Name: name,
        Email: email,
        Phone: phone,
        Country: country,
        State: state,
        City: city,
        Gender: gender
    };

     $.ajax({
        url: '/login/Insert',
        type: 'POST',
        data: formData, // Sending as application/x-www-form-urlencoded
        success: function (data) {
            $('#result').html(data.message || "Registration successful!");
            Swal.fire({
                title: "Registration",
                text: "Successful!!",
                icon: "success"
            });

                        window.location.href = "/abc/dashboard";
         },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    });
});


function getStates() {
    var countryId = $('#country').val();

    $.ajax({
        url: '/login/GetStates',
        type: 'GET',
        data: { countryId: countryId },
        success: function (states) {
            console.log(states);
            $('#state').empty().append('<option value="">--Select State--</option>');
            $.each(states, function (i, state) {
                $('#state').append('<option value="' + state.Value + '">' + state.Text + '</option>');
            });
            $('#city').empty().append('<option value="">--Select City--</option>');
        }
    });
}

function getCities() {
    var stateId = $('#state').val();

    $.ajax({
        url: '/login/GetCities',
        type: 'GET',
        data: { stateId: stateId },
        success: function (cities) {
            $('#city').empty().append('<option value="">--Select City--</option>');
            $.each(cities, function (i, city) {
                $('#city').append('<option value="' + city.Value + '">' + city.Text + '</option>');
            });
        }
    });
}