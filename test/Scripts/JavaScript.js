document.getElementById('data').addEventListener('submit', function (e) {
    e.preventDefault();

    const userid = document.getElementById('userid').value.trim();
    const fullname = document.getElementById('fullname').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();
    const state = document.getElementById('state').value.trim();
    const city = document.getElementById('city').value.trim();
    const gender = document.querySelector('input[name="gender"]:checked').value;
    const course = document.getElementById('course').value.trim();
    const password = document.getElementById('password').value.trim();

    const matter = {
        userid: userid,
        fullname: fullname,
        email: email,
        phone: phone,
        state: state,
        city: city,
        gender: gender,
        course: course,
        password: password
    };

    if (userid === "0") {
        $.ajax({
            url: '/Default/insert',
            type: 'POST',
            data: matter, // Sending as application/x-www-form-urlencoded
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
            data: matter,
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
