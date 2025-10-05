document.getElementById('longForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const userid = document.getElementById('userid').value.trim();
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const address = document.getElementById('address').value.trim();
    const city = document.getElementById('city').value.trim();
    const state = document.getElementById('state').value.trim();
    const gender = document.querySelector('input[name="gender"]:checked').value;
    const message = document.getElementById('message').value.trim();

    const formData = {
        userid: userid,
        name: name,
        Email: email,
        address: address,
        city: city,
        State: state,
        gender: gender,
        message: message
    };
    if (userid === "0") {
        $.ajax({
            url: '/ashu/insert',
            type: 'POST',
            data: formData, // Sending as application/x-www-form-urlencoded
            success: function (data) {
              
                
                window.location.href = "/ashu/listing";
            },

            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
    else {
        debugger;

        $.ajax({
            url: '/ashu/update', // Adjust to your actual controller/action
            type: 'POST',
            data: formData,
            success: function (data) {
                $('#result').html(data.message || "Update successful!");
                alert("Updates Successful!");
                window.location.href = '/ashu/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }


});

