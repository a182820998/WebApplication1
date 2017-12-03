$(function() {
    $("#button_login").click(function() {

        var postObject = {
            Id: $("#text_1").val(),
            Username: $("#text_2").val(),
            Password: $("#text_3").val()
        };

        $.ajax({
            url: '/Customer/PostResult',
            type: 'Post',
            data: JSON.stringify(postObject),
            contentType: 'application/json',
            dataType: 'json',
            error: function (response) {
                alert(response.responseText);
            },
            success: function (response) {
                alert(
                    "Your userid is " + response['Id'] + "\n" +
                    "Your username is " + response['Username'] + "\n" +
                    "Your password is " + response['Password']
                );
            }
        });
    });
});

$(function() {
    $("#button_vip").click(function() {

        $.ajax({
            url: '/VIP/GetResult',
            type: 'GET',
            data: {
                Username: "GetTestUser",
                Password: "GetTestPass"
            },
            contentType: 'application/json',
            dataType: 'json',
            error: function(response) {
                alert(response.responseText);
            },
            success: function(response) {
                alert(
                    "Your username is " + response['Username'] + "\n" +
                    "Your password is " + response['Password']
                );
            }
        });

    });
});