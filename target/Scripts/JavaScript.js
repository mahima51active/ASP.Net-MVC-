document.getElementById('main').addEventListener('submit', function (e){
    e.preventDefault();

    const userid = document.getElementById('userid').value.trim();
    const Employeename = document.getElementById('name').value.trim();
    const Email = document.getElementById('email').value.trim();
    const Phone = document.getElementById('phone').value.trim();
    const Course = document.getElementById('course').value.trim();
    const Dob = document.getElementById('dob').value.trim();
    const Address = document.getElementById('address').value.trim();
    const Gender = document.querySelector('input[name="gender"]:checked').value;
    const City = document.getElementById('city').value.trim();
    const State = document.getElementById('state').value.trim();
    const Country = document.getElementById('country').value.trim();
    const Message = document.getElementById('message').value.trim();

    const Detail = {
        userid: userid,
        Employeename: Employeename,
        Email: Email,   
        Phone: Phone,
        Course: Course,
        Dob: Dob,
        Address: Address,
        Gender: Gender,
        City: City,
        State: State,
        Country: Country,
        Message: Message
    };

    if (userid === "0") {
        $.ajax({
            url: '/Default/insert',
            type: 'POST',
            data: Detail, // Sending as application/x-www-form-urlencoded
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
            data: Detail,
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
