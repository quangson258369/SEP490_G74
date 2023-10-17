function displayImage() {
    const imageInput = document.getElementById('image-input');
    const uploadedImage = document.getElementById('uploaded-image');
    if (imageInput.files && imageInput.files[0]) {
      const reader = new FileReader();
      reader.onload = function(e) {
        uploadedImage.src = e.target.result;
      };
      reader.readAsDataURL(imageInput.files[0]);
    }
  }

  const imageContainer = document.getElementById('image-container');
  imageContainer.addEventListener('click', function() {
    const imageInput = document.getElementById('image-input');
    imageInput.click();
  });