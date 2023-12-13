$(document).ready(function () {
    // Hide additional options on page load
    $(".line5").hide();

    // Handle change event on role selection
    $("#roleSelection").change(function () {
        // Hide all additional options
        $(".line5").hide();

        // Get the selected value
        var selectedRole = $(this).val();
        console.log(selectedRole)
        // Show additional options only if Bác Sĩ is selected
        if (selectedRole === "2") {
            $("#doctorOptions").show();

        }
    });
});

function displayImage() {
    const imageInput = document.getElementById('image-input');
    const uploadedImage = document.getElementById('uploaded-image');
    if (imageInput.files && imageInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            uploadedImage.src = e.target.result;
        };
        reader.readAsDataURL(imageInput.files[0]);
    }
}

const imageContainer = document.getElementById('image-container');
imageContainer.addEventListener('click', function () {
    const imageInput = document.getElementById('image-input');
    imageInput.click();
});

function loadModalAndShow() {
    $.get("PopUpSuccess.html", function (data) {
        $("body").append(data);
        $("#successModal").modal("show");
    });
}


