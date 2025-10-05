let debounceTimer;

$('#txtempfirstname').keyup(function (event) {
    clearTimeout(debounceTimer); // Clear the previous timer

    debounceTimer = setTimeout(function () {
        let value = event.target.value;

        console.log("User finished typing: " + value);
        //alert("User finished typing: " + value);
        $.ajax({
            url: '/Default/empcode',       // URL to send the request to
            method: 'POST',              // or 'POST', 'PUT', 'DELETE'
            data: { empname: value },   // Send the value with the key 'empname'
            success: function (response) {
                console.log('Success:', response);
                $('#txtempid').val(response);

            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });


    }, 500); // 500ms debounce delay
});
