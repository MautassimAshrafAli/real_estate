
function EmailCheck() {
    $("#status").html("Checking...");
    $.post("@Url.Action("checkEmailAvailability", "users")",
        {
            email: $("#email").val()
        },
        function (data) {
            if (data == 0) {
                document.getElementById("submint").disabled = false;
                $("#status").html('<font color="Green">Email is available</font>');
                $("#email").css("border-color", "Green");
            } else {
                document.getElementById("submint").disabled = true;
                $("#status").html('<font color="Red">That email is already exists. Try Another email</font>');
                $("#email").css("border-color", "Red");
            }
        });
}
