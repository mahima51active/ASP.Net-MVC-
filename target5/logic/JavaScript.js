document.getElementById('main').addEventListener("submit", function (e) {
    e.preventDefault();

    const userid = document.getElementById('userid').value.trim();
    const fullname = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const gender = document.querySelector('input[name="gender"]:checked').value;
    const course = document.getElementById('course').value.trim();
    const city = document.getElementById('city').value.trim();
    const address = document.getElementById('address').value.trim();
    const state = document.getElementById('state').value.trim();
    const country = document.getElementById('country').value.trim();
    const comment = document.getElementById('comment').value.trim();

    const data = {
        userid: userid,
        fullname: fullname,
        email: email,
        phone: phone,
        gender: gender,
        course: course,
        city: city,
        address: address,
        state: state,
        country: country,
        comment: comment
    };

    if (userid === "0") {
        $.ajax({
            url: '/Default/insert',
            type: 'POST',
            data: data, // Sending as application/x-www-form-urlencoded
            success: function (data) {


                window.location.href = "/Default/listing";
            },

            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
    else {
        debugger;

        $.ajax({
            url: '/Default/update', // Adjust to your actual controller/action
            type: 'POST',
            data: data,
            success: function (data) {
                $('#result').html(data.message || "Update successful!");
                alert("Updates Successful!");
                window.location.href = '/Default/listing';
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }


});


