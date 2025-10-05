document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");

    form.addEventListener("submit", function (e) {
        e.preventDefault(); // Stop actual submission

        let isValid = true;
        let messages = [];

        const inputs = form.querySelectorAll("input, select, textarea");
        const aadhar = inputs[4];
        const email = inputs[5];
        const phone = inputs[6];
        const highSchool = inputs[13];
        const intermediate = inputs[14];
        const termsCheckbox = inputs[20];
        const userid = '';

        // Required validation
        inputs.forEach((input) => {
            if (input.hasAttribute("required") && !input.value.trim()) {
                isValid = false;
                messages.push(`${input.previousElementSibling.innerText} is required.`);
            }
        });

        // Aadhar check
        if (aadhar.value.trim().length !== 12 || isNaN(aadhar.value)) {
            isValid = false;
            messages.push("Aadhar Number must be a 12-digit number.");
        }

        // Email check
        debugger;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email.value.trim())) {
            isValid = false;
            messages.push("Enter a valid Email address.");
        }

        // Phone check
        const phoneRegex = /^\d{10}$/;
        if (!phoneRegex.test(phone.value.trim())) {
            isValid = false;
            messages.push("Enter a valid 10-digit Phone Number.");
        }

        // Percentage fields
        const percentFields = [highSchool, intermediate];
        percentFields.forEach((field) => {
            const val = parseFloat(field.value);
            if (field.value && (isNaN(val) || val < 0 || val > 100)) {
                isValid = false;
                messages.push(`${field.previousElementSibling.innerText} must be between 0 and 100.`);
            }
        });

        // Terms check
        if (!termsCheckbox.checked) {
            isValid = false;
            messages.push("You must confirm that the information is true.");
        }

        // Final validation
        //if (!isValid) {
        //    alert(messages.join("\n"));
        //} else {
        //    alert("✅ Successfully submitted!");
        //    form.reset(); // Clear form
        //}


        // Prepare data
        const formData = {
            userid: userid,
            Name: document.getElementById('txtname').value,
            last_name: document.getElementById('txtlst').getAttribute('value'),
            dob: document.getElementById('txtdob').getAttribute('value'),
            gender: document.getElementById('txtgen').getAttribute('value'),
            Adhar: document.getElementById('txtadhr').getAttribute('value'),
            Email: document.getElementById('txteml').getAttribute('value'),
            phone: document.getElementById('txtphn').getAttribute('value'),
            Address: document.getElementById('txtaddres').getAttribute('value'),
            City: document.getElementById('txtcity').getAttribute('value'),
            State: document.getElementById('txtstate').getAttribute('value'),
            zip: document.getElementById('txtzip').getAttribute('value'),
            highschool: document.getElementById('txthigh').getAttribute('value'),
            Intermediate: document.getElementById('txtinter').getAttribute('value'),
            Graduation: document.getElementById('txtug').getAttribute('value'),
            PostGraduation: document.getElementById('txtpg').getAttribute('value'),
            Academic: document.getElementById('txtacd').getAttribute('value')  
        };
        console.log(formData);
        debugger;
        // Send AJAX request
        //if (userid == "0" || userid == "") {
            $.ajax({
                url: '/StudentR/Insert',  
                type: 'POST',
                 data: formData,
                success: function (data) {
                    $('#result').html(data.message || "Registration successful!");
                    alert("Registration Successful!");
                    document.getElementById('registrationForm').reset();

                     window.location.href = '/StudentR/listing';
                },
                error: function (xhr, status, error) {
                    console.error("Error:", error);
                }
            });
        //}
        //else {
        //    $.ajax({
        //        url: '/Form/update', // Adjust to your actual controller/action
        //        type: 'POST',
        //        data: formData,
        //        success: function (data) {
        //            $('#result').html(data.message || "Update successful!");
        //            alert("Updates Successful!");
        //            document.getElementById('registrationForm').reset();

        //            window.location.href = '/Form/listing';
        //        },
        //        error: function (xhr, status, error) {
        //            console.error("Error:", error);
        //        }
        //    });

        //}

    });
});




